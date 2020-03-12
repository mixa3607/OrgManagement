using System.ComponentModel.DataAnnotations;
using System.Linq;
using AuthWebApi.Database;
using Microsoft.Extensions.DependencyInjection;

namespace AuthWebApi.Attributes
{
    public class UserExistAttribute : ValidationAttribute
    {
        private readonly bool _wrongIfExist;
        public UserExistAttribute(bool wrongIfExist = false)
        {
            _wrongIfExist = wrongIfExist;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string userName))
            {
                return new ValidationResult("User name not set");
            }
            var db = validationContext.GetService<AuthDbContext>();
            if (db.Users.Any(u => u.UserName == userName))
                return !_wrongIfExist ? ValidationResult.Success : new ValidationResult("User already exist");
            else
                return _wrongIfExist ? ValidationResult.Success : new ValidationResult("User not exist");
        }
    }
}