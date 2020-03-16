using System;

namespace ManagementWebApi.DataModels.UpdateModels
{
    public class PassportUpdate
    {
        public uint Batch { get; set; }
        public uint SerialNumber { get; set; }
        public string Issuer { get; set; }
        public uint IssuerNum { get; set; }
        public DateTime? IssuedAt { get; set; }
        public string RegPlace { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}