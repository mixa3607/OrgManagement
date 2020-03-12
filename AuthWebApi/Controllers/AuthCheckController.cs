using System;
using AuthWebApi.Attributes;
using AuthWebApi.Database;
using Microsoft.AspNetCore.Mvc;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Controllers
{
    [ApiController]
    [Route(GlobalConst.ApiRoot + "/[controller]")]
    public class AuthCheckController : ControllerBase
    {
        [HttpGet("user")]
        [AuthorizeEnum(EUserRole.User)]
        public IActionResult UserAuthCheckGet() => Ok();

        [HttpGet("admin")]
        [AuthorizeEnum(EUserRole.Admin)]
        public IActionResult AdminAuthCheckGet() => Ok();
    }
}