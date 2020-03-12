using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public class JwtGeneratorService : ITokenGeneratorService
    {
        private readonly AuthOptions _opts;

        public JwtGeneratorService(IOptions<AuthOptions> opts)
        {
            _opts = opts.Value;
            _opts.Init();
        }

        public string GenerateToken(string userName, EUserRole userRole)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.ToString()),
            };
            var identity = new ClaimsIdentity(claims, "Token");

            var token = new JwtSecurityToken(_opts.JwtIssuer,
                _opts.JwtAudience,
                identity.Claims,
                now,
                now.AddMinutes(_opts.JwtLifetimeMins),
                new SigningCredentials(_opts.PrivateKey, SecurityAlgorithms.RsaSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}