using System;

namespace ManagementWebApi.DataModels
{
    public class CertUploadResult : IUploadResult
    {
        public long Id { get; set; }
        public string Hash { get; set; }

        public string Issuer { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }
    }
}