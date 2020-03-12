using System.ComponentModel.DataAnnotations;
using AuthWebApi.Attributes;
using WebApiSharedParts.Attributes;
using WebApiSharedParts.Enums;

namespace AuthWebApi.DataModels
{
    public class UserRegistrationModels
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-z0-9]*$")]
        [UserExist(true)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[^ ]*$")]
        public string Fingerprint { get; set; }
    }
}