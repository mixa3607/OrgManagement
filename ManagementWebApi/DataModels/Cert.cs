using System;

namespace ManagementWebApi.DataModels
{
    public class Cert : ICert
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }
        public string Issuer { get; set; }

        //public long? CertFileId { get; set; }
        public File CertFile { get; set; }

        //public long? ContainerFileId { get; set; }
        public File ContainerFile { get; set; }
    }
}