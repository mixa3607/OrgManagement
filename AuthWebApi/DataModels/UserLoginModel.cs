using System.ComponentModel.DataAnnotations;
using AuthWebApi.Attributes;

namespace AuthWebApi.DataModels
{
    public class UserLoginModel
    {
        [Required(AllowEmptyStrings = false)]
        [UserExist]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Fingerprint { get; set; }
    }
}