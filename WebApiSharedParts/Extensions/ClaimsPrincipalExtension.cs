using System;
using System.Security.Claims;
using WebApiSharedParts.Enums;

namespace WebApiSharedParts.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(EUserRole.Admin.ToString());
        }
    }
}