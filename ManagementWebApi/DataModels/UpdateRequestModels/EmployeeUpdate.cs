using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementWebApi.DataModels.UpdateModels
{
    public class EmployeeUpdate
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkingPosition { get; set; }
        public string Ipv4Address { get; set; }
        public string DomainNameEntry { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
