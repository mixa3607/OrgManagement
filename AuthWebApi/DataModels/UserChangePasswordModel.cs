using System;
using System.ComponentModel.DataAnnotations;
using AuthWebApi.Attributes;
using WebApiSharedParts.Attributes;

namespace AuthWebApi.DataModels
{
    public class UserChangePasswordModel
    {
        [Required(AllowEmptyStrings = false)]
        [NotEqual(nameof(NewPassword))]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Fingerprint { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid? RefreshToken { get; set; }
    }
}