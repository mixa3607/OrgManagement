using System.Collections.Generic;
using ManagementWebApi.DataModels.ListModels;

namespace ManagementWebApi.DataModels.DetailedModels
{
    public class EmployeeDt
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkingPosition { get; set; }
        public string Ipv4Address { get; set; }
        public bool IsOnline { get; set; } = false;
        public string DomainNameEntry { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Passport Passport { get; set; }
        public TaxId TaxId { get; set; }

        public IEnumerable<CertL> Certs { get; set; }
        public IEnumerable<DeviceL> Devices { get; set; }
    }
}