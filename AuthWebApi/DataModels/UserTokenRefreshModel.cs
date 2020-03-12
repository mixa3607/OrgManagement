using System;
using System.ComponentModel.DataAnnotations;
using AuthWebApi.Attributes;
using WebApiSharedParts.Attributes;

namespace AuthWebApi.DataModels
{
    public class UserTokenRefreshModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fingerprint cant be empty")]
        public string Fingerprint { get; set; }

        [Required(ErrorMessage = "Refresh token cant be empty")]
        [ValidRefreshToken(nameof(Fingerprint))]
        public Guid? RefreshToken { get; set; }
    }
}