﻿using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbEmployee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkingPosition { get; set; }
        public string Ipv4Address { get; set; }
        public string DomainNameEntry { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; } = false;

        public long PassportId { get; set; }
        public DbPassport NavPassport { get; set; }

        public long TaxIdId { get; set; }
        public DbTaxId NavTaxId { get; set; }

        public List<DbDevice> NavDevices { get; set; }
        public List<DbCert> NavCerts { get; set; }
    }
}
