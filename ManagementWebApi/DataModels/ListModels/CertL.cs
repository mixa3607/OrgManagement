using System;

namespace ManagementWebApi.DataModels.ListModels
{
    public class CertL
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
        public string Issuer { get; set; }

        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
    }
}