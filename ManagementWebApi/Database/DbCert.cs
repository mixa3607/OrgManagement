using System;

namespace ManagementWebApi.Database
{
    public class DbCert
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }
        public string Issuer { get; set; }

        public long CertFileId { get; set; }
        public DbFile NavCertFile { get; set; }

        public long ContainerFileId { get; set; }
        public DbFile NavContainerFile { get; set; }

        public long EmployeeId { get; set; }
        public DbEmployee NavEmployee { get; set; }
    }
}
