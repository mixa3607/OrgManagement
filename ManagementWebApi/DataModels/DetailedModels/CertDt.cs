using System;

namespace ManagementWebApi.DataModels.DetailedModels
{
    public class CertDt
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
        public string Issuer { get; set; }

        public long CertFileId { get; set; }
        public long? ContainerFileId { get; set; }

        public long EmployeeId { get; set; }
    }
}