using System;

namespace ManagementWebApi.DataModels
{
    public interface ICert
    {
        long? Id { get; set; }
        string Name { get; set; }
        DateTime? NotBefore { get; set; }
        DateTime? NotAfter { get; set; }
        string Issuer { get; set; }
        File CertFile { get; set; }
        File ContainerFile { get; set; }
    }
}