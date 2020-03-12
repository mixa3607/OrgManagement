using System;
using System.Collections.Generic;
using System.Linq;
using AuthWebApi.Database;
using AuthWebApi.DataModels;
using AuthWebApi.Extensions;
using AuthWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;
using WebApiSharedParts.Extensions;

namespace AuthWebApi.Controllers
{
    [Route(GlobalConst.ApiRoot + "/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _db;
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly IRngGeneratorService _rngGenerator;
        private readonly DateTime _now = DateTime.UtcNow;
        private readonly AuthOptions _authOptions;
        private readonly ILogger<AuthController> _logger;

        private string UserAgent => Request.GetUserAgent();
        private string UserIp => Request.GetIp();
        private string UserName => User.Identity.Name;

        public AuthController(AuthDbContext dbContext,
            IOptions<AuthOptions> authOptions,
            ITokenGeneratorService tokenGenerator,
            IRngGeneratorService rngGenerator,
            ILogger<AuthController> logger)
        {
            _logger = logger;
            _rngGenerator = rngGenerator;
            _tokenGenerator = tokenGenerator;
            _db = dbContext;
            _authOptions = authOptions.Value;
        }

        [HttpPut("registration")]
        [AllowAnonymous]
        public IActionResult Registration([FromBody]UserRegistrationModels registrationModel, [FromServices] IChallengeNotifierService game)
        {
            _logger.LogDebug("Registration attempt from {RemoteIp} and {UserAgent}", UserIp, UserAgent);
            var challenge = _rngGenerator.GetString(6, EAlphabetType.AZ);
            game.Notify(registrationModel.UserName, challenge);
            //create user entry and refresh token

            var dbUser = registrationModel.ToDbUser();
            dbUser.Challenge = challenge;
            dbUser.CreateDateTime = _now;
            dbUser.State = EUserState.InChallenge;
            dbUser.HashPasswordAndSet(registrationModel.Password);

            try
            {
                _logger.LogDebug("Adding new user ({UserName})  to DB", dbUser.UserName);
                _db.Users.Add(dbUser);
                _db.SaveChanges();
                _logger.LogDebug("User ({UserName}) successful added", dbUser.UserName);
                return Ok();
            }
            catch (SecurityTokenEncryptionFailedException e)
            {
                _logger.LogError(e, "Problem with generating JWT for {UserName}", dbUser.UserName);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem with saving user (@{User})", dbUser);
                throw;
            }
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login([FromQuery]UserLoginModel loginModel)
        {
            _logger.LogTrace("Login attempt from {RemoteIp} and {UserAgent} with user name {UserName}", UserIp, UserAgent, loginModel.UserName);
            var dbUser = _db.Users.FirstOrDefault(u => u.UserName == loginModel.UserName);
            if (dbUser == null)
            {
                //TODO: maybe change to generalized error
                _logger.LogTrace("User name {UserName} not exist", loginModel.UserName);
                ModelState.AddModelError("User", "user not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            try
            {
                if (dbUser.State == EUserState.InChallenge)
                {
                    return Forbid(new AuthenticationProperties(new Dictionary<string, string> { { "Challenge", "need pass" } }));
                }
                switch (dbUser.VerifyPassword(loginModel.Password))
                {
                    case PasswordVerificationResult.Failed:
                        _logger.LogTrace("User {UserName} send wrong password", loginModel.UserName);
                        ModelState.AddModelError("Password", "Wrong password");
                        return BadRequest(new ValidationProblemDetails(ModelState));
                    case PasswordVerificationResult.SuccessRehashNeeded:
                        {
                            dbUser.HashPasswordAndSet(loginModel.Password);
                            _db.Users.Update(dbUser);
                            break;
                        }
                }

                var refreshToken = new DbRefreshToken()
                {
                    Fingerprint = loginModel.Fingerprint,
                    FromTime = _now,
                    ToTime = _now.AddDays(_authOptions.RefreshTokenLifetimeDays),
                    CreatedIp = UserIp,
                    NavUser = dbUser,
                    UserAgent = UserAgent,
                    Token = _rngGenerator.GetGuid()
                };

                _db.RefreshTokens.Add(refreshToken);
                _db.SaveChanges();

                var jwt = _tokenGenerator.GenerateToken(dbUser.UserName, dbUser.Role);
                return Ok(new AuthResult()
                {
                    JwtToken = jwt,
                    RefreshToken = refreshToken.Token,
                    UserName = dbUser.UserName
                });
            }
            catch (SecurityTokenEncryptionFailedException e)
            {
                _logger.LogError(e, "Problem with generating JWT for {UserName}", dbUser.UserName);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem with applying changes to db");
                throw;
            }
        }

        [HttpDelete("logout")]
        [AuthorizeEnum(EUserRole.User, EUserRole.Admin)]
        public IActionResult Logout(Guid refreshToken)
        {
            _logger.LogTrace("Logout attempt from {RemoteIp} and {UserAgent} with user name {UserName} for token {RefreshToken}", UserIp, UserAgent, UserName, refreshToken);
            var token = _db.RefreshTokens.FirstOrDefault(rt => rt.NavUser.UserName == UserName && rt.Token == refreshToken);
            if (token == null)
            {
                _logger.LogTrace("Invalid refresh token {RefreshToken} for user {UserName}", refreshToken, UserName);
                ModelState.AddModelError("Refresh token", "Invalid token");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            token.UsedIp = UserIp;
            token.UsedDateTime = _now;
            try
            {
                _db.RefreshTokens.Update(token);
                _db.SaveChanges();
                _logger.LogTrace("Refresh token {RefreshToken} successfully expired", refreshToken);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "Problem with updating {RefreshToken} refresh token", refreshToken);
                throw;
            }
            return Ok();
        }

        [HttpPost("password")]
        [AuthorizeEnum(EUserRole.Admin, EUserRole.User)]
        public IActionResult ChangePassword([FromQuery]UserChangePasswordModel changePasswordModel)
        {
            _logger.LogTrace("Login attempt from {RemoteIp} and {UserAgent} with user name {UserName}", UserIp, UserAgent, UserName);
            var dbUser = _db.Users.FirstOrDefault(u => u.UserName == UserName);
            if (dbUser == null)
            {
                _logger.LogTrace("User name {UserName} not exist", UserName);
                ModelState.AddModelError("User", "user not found");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            //get all valid refresh tokens
            var dbTokens = _db.RefreshTokens
                .Include(rt => rt.NavUser)
                .Where(rt =>
                    rt.UsedDateTime == null &&
                    _now < rt.ToTime &&
                    _now > rt.FromTime)
                .ToArray();

            var targetToken = dbTokens.FirstOrDefault(rt => rt.Token == changePasswordModel.RefreshToken &&
                                                            rt.Fingerprint == changePasswordModel.Fingerprint &&
                                                            rt.UserAgent == UserAgent);
            if (targetToken == null)
            {
                _logger.LogTrace("Invalid refresh token for {UserName}", UserName);
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (dbUser.VerifyPassword(changePasswordModel.OldPassword) == PasswordVerificationResult.Failed)
            {
                _logger.LogTrace("User {UserName} send wrong password", UserName);
                ModelState.AddModelError("Old password", "Old password wrong");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            //update password
            dbUser.HashPasswordAndSet(changePasswordModel.NewPassword);
            dbUser.LastPassChangeDateTime = _now;

            //revoke old tokens
            foreach (var dbRefreshToken in dbTokens)
            {
                dbRefreshToken.UsedDateTime = _now;
                dbRefreshToken.UsedIp = UserIp;
            }
            try
            {
                _db.Users.Update(dbUser);
                _db.RefreshTokens.UpdateRange(dbTokens);
                _db.SaveChanges();
                return Ok();
            }
            catch (SecurityTokenEncryptionFailedException e)
            {
                _logger.LogError(e, "Problem with generating JWT for {UserName}", dbUser.UserName);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem with applying changes to db");
                throw;
            }
        }

        [HttpPost("challenge")]
        [AllowAnonymous]
        public IActionResult PassChallenge([FromBody]UserPassChallengeModel passChallengeModel)
        {
            _logger.LogTrace("Pass challenge attempt from {RemoteIp} and {UserAgent} with user name {UserName}", UserIp, UserAgent, passChallengeModel.UserName);
            var dbUser = _db.Users.FirstOrDefault(u => u.UserName == passChallengeModel.UserName);
            if (dbUser.VerifyPassword(passChallengeModel.Password) == PasswordVerificationResult.Failed)
            {
                _logger.LogTrace("User {UserName} send wrong password", UserName);
                ModelState.AddModelError("Password", "Password wrong");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            if (dbUser.State == EUserState.InChallenge)
            {
                if (dbUser.Challenge == passChallengeModel.Challenge)
                {
                    dbUser.State = EUserState.Ok;
                    _db.Users.Update(dbUser);
                    _db.SaveChanges();
                }
                else
                {
                    _logger.LogTrace("User {UserName} send wrong challenge string. Excepted {ExpChallenge} but get {GetChallenge}", UserName, dbUser.Challenge, passChallengeModel.Challenge);
                    ModelState.AddModelError("User", "wrong challenge string");
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }
            }
            else
            {
                _logger.LogTrace("User {UserName} already pass challenge", UserName);
                ModelState.AddModelError("User", "challenge already passed");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            return Ok();
        }

        [HttpGet("refreshToken")]
        [AllowAnonymous]
        public IActionResult Refresh([FromQuery]UserTokenRefreshModel userRefreshModel)
        {
            _logger.LogTrace("Update session attempt from {RemoteIp} and {UserAgent} with refresh token {RefreshToken}", UserIp, UserAgent, userRefreshModel.RefreshToken);
            var dbToken = _db.RefreshTokens
                .Include(rt => rt.NavUser)
                .FirstOrDefault(rt => rt.Token == userRefreshModel.RefreshToken);

            if (dbToken == null)
            {
                _logger.LogTrace("Invalid refresh token. Need re:login");
                ModelState.AddModelError("RefreshToken", "Invalid refresh token");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            dbToken.UsedDateTime = _now;
            dbToken.UsedIp = UserIp;
            var dbUser = dbToken.NavUser;
            var newRefreshToken = new DbRefreshToken()
            {
                Fingerprint = userRefreshModel.Fingerprint,
                FromTime = _now,
                ToTime = _now.AddDays(_authOptions.RefreshTokenLifetimeDays),
                CreatedIp = UserIp,
                NavUser = dbUser,
                UserAgent = UserAgent,
                Token = _rngGenerator.GetGuid()
            };

            try
            {
                _db.RefreshTokens.Add(newRefreshToken);
                _db.RefreshTokens.Update(dbToken);
                _db.SaveChanges();
                var jwt = _tokenGenerator.GenerateToken(dbUser.UserName, dbUser.Role);

                _logger.LogDebug("Old tokens expired, new created ({RefreshToken}) and JWT generated for user: {UserName}", newRefreshToken.Token, dbUser.UserName);
                return Ok(new AuthResult()
                {
                    JwtToken = jwt,
                    UserName = dbUser.UserName,
                    RefreshToken = newRefreshToken.Token
                });
            }
            catch (SecurityTokenEncryptionFailedException e)
            {
                _logger.LogError(e, "Problem with generating JWT for {UserName}", dbUser.UserName);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Problem with applying changes to db");
                throw;
            }
        }
    }
}