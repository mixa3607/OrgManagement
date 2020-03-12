using System;

namespace AuthWebApi.Database
{
    public class DbRefreshToken
    {
        public Guid Token { get; set; }
        public long UserId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public string UserAgent { get; set; }
        public string Fingerprint { get; set; }
        public string CreatedIp { get; set; }

        public DateTime? UsedDateTime { get; set; }
        public string UsedIp { get; set; }

        public DbUser NavUser { get; set; }
    }
}