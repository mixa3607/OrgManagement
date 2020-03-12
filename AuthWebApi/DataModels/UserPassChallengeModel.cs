using System.ComponentModel.DataAnnotations;
using AuthWebApi.Attributes;

namespace AuthWebApi.DataModels
{
    public class UserPassChallengeModel
    {
        [Required(AllowEmptyStrings = false)]
        [UserExist]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Challenge { get; set; }
    }
}