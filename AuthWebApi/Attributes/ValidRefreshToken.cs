using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AuthWebApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AuthWebApi.Attributes
{
    public class ValidRefreshToken : ValidationAttribute
    {
        private readonly string _fingerprintPropertyName;
        public ValidRefreshToken(string fingerprintPropertyName)
        {
            _fingerprintPropertyName = fingerprintPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is Guid refreshToken))
                return new ValidationResult("Refresh token not set");
            var fingerprintPropertyInfo = validationContext.ObjectType.GetProperty(_fingerprintPropertyName);
            if (fingerprintPropertyInfo != null)
            {
                if (!(fingerprintPropertyInfo.GetValue(validationContext.ObjectInstance, null) is string fingerprint))
                    return new ValidationResult("Fingerprint not set");

                var httpContext = validationContext.GetService<IHttpContextAccessor>().HttpContext;
                var userAgent = (string) httpContext.Request.Headers["User-Agent"];
                var db = validationContext.GetService<AuthDbContext>();
                var now = DateTime.Now;

                var dbToken = db.RefreshTokens
                    .Any(rt =>
                        rt.UsedDateTime == null &&
                        now < rt.ToTime &&
                        now > rt.FromTime &&
                        rt.Token == refreshToken &&
                        rt.Fingerprint == fingerprint &&
                        rt.UserAgent == userAgent);

                return dbToken ? ValidationResult.Success : new ValidationResult("Invalid refresh token");
            }
            else
            {
                throw new ArgumentException($"Property {_fingerprintPropertyName} not define in target class");
            }
        }
    }
}