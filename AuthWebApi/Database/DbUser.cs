using System;
using System.Collections.Generic;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Database
{
    public class DbUser
    {
        public long Id { get; set; }

        public string UserName { get; set; }
        public string PassHash { get; set; }
        public EUserRole Role { get; set; } = EUserRole.Admin;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public string Challenge { get; set; }
        public EUserState State { get; set; } = EUserState.InChallenge;
        
        public DateTime? LastPassChangeDateTime { get; set; }


        public List<DbRefreshToken> NavRefreshTokens { get; set; }
    }
}