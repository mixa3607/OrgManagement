using System.ComponentModel.DataAnnotations;
using System.Linq;
using AuthWebApi.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthWebApi.Controllers
{
    [Route(GlobalConst.ApiRoot + "/[controller]")]
    [ApiController]
    public class UserNameController : ControllerBase
    {
        [HttpGet("check")]
        [AllowAnonymous]
        public IActionResult CheckUsername(
            [Required(AllowEmptyStrings = false, ErrorMessage = "can not be empty")]
            [RegularExpression("^[A-z0-9]*$")]
            [FromQuery] string userName, 
            [FromServices] AuthDbContext db,
            [FromServices] ILogger<UserNameController> logger)
        {
            logger.LogTrace("Check user name ({UserName}) from {RemoteIp}", userName, HttpContext.Connection.RemoteIpAddress);
            if (db.Users.FirstOrDefault(u => u.UserName == userName) == null) 
                return Ok();

            ModelState.AddModelError("User name", "user name used");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}