﻿using System.Collections.Generic;

namespace ManagementWebApi.DataModels
{
    public class Employee
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkingPosition { get; set; }
        public string Ipv4Address { get; set; }
        public bool IsOnline { get; set; } = false;
        public string DomainNameEntry { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        
        //public long? PassportId { get; set; }
        public Passport Passport { get; set; }

        //public long? TaxIdId { get; set; }
        public TaxId TaxId { get; set; }

        public IEnumerable<Cert> Certs { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}