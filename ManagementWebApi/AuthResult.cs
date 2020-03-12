using System;

namespace ManagementWebApi
{
    public class AuthResult
    {
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}