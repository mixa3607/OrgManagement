using System;

namespace ManagementWebApi.Database
{
    public class DbPassport
    {
        public long Id { get; set; }
        public uint Batch { get; set; }
        public uint SerialNumber { get; set; }
        public string Issuer { get; set; }
        public uint IssuerNum { get; set; }
        public DateTime? IssuedAt { get; set; }
        public string RegPlace { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDay { get; set; }

        public long ScanFileId { get; set; }
        public DbFile NavScanFile { get; set; }

        //public long EmployeeId { get; set; }
        public DbEmployee NavEmployee { get; set; }
    }
}
