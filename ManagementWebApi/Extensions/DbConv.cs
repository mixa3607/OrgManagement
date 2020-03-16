using System.Linq;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;

namespace ManagementWebApi.Extensions
{
    public static class DbConv
    {
        public static Employee ToModel(this DbEmployee dbEmployee)
        {
            return new Employee()
            {
                Id = dbEmployee.Id,
                Initials = dbEmployee.Initials,
                Department = dbEmployee.Department,
                DomainNameEntry = dbEmployee.DomainNameEntry,
                Email = dbEmployee.Email,
                WorkingPosition = dbEmployee.WorkingPosition,
                PhoneNumber = dbEmployee.PhoneNumber,
                Ipv4Address = dbEmployee.Ipv4StrAddress,
                Passport = dbEmployee.NavPassport.ToModel(),
                TaxId = dbEmployee.NavTaxId.ToModel(),
                Certs = dbEmployee.NavCerts?.Select(x=>x.ToModel()),
                Devices = dbEmployee.NavDevices?.Select(x=>x.ToModel())
            };
        }

        public static Passport ToModel(this DbPassport dbPassport)
        {
            return dbPassport == null
                ? null
                : new Passport()
                {
                    Id = dbPassport.Id,
                    Batch = dbPassport.Batch,
                    Issuer = dbPassport.Issuer,
                    BirthPlace = dbPassport.BirthPlace,
                    BirthDay = dbPassport.BirthDay,
                    IssuedAt = dbPassport.IssuedAt,
                    IssuerNum = dbPassport.IssuerNum,
                    RegPlace = dbPassport.RegPlace,
                    SerialNumber = dbPassport.SerialNumber,
                    ScanFile = dbPassport.NavScanFile.ToModel(),
                };
        }

        public static TaxId ToModel(this DbTaxId dbTax)
        {
            return dbTax == null
                ? null
                : new TaxId()
                {
                    SerialNumber = dbTax.StrSerialNumber,
                    Id = dbTax.Id,
                    TaxIdScan = dbTax.NavTaxIdScan.ToModel(),
                };
        }

        public static Cert ToModel(this DbCert dbCert)
        {
            return dbCert == null
                ? null
                : new Cert()
                {
                    Id = dbCert.Id,
                    Name = dbCert.Name,
                    NotAfter = dbCert.NotAfter,
                    NotBefore = dbCert.NotBefore,
                    Issuer = dbCert.Issuer,
                    CertFile = dbCert.NavCertFile.ToModel(),
                    ContainerFile = dbCert.NavContainerFile.ToModel(),
                };
        }

        public static Device ToModel(this DbDevice dbDevice)
        {
            return dbDevice == null
                ? null
                : new Device()
                {
                    Id = dbDevice.Id,
                    InvNumber = dbDevice.InvNumber,
                    Name = dbDevice.Name,
                    Type = dbDevice.NavDeviceType.Name,
                    Actions = dbDevice.NavActions?.Select(x=>x.ToModel()),
                    Softwares = dbDevice.NavSoftwares?.Select(x=>x.ToModel()),
                };
        }

        public static Software ToModel(this DbSoftware dbSoftware)
        {
            return dbSoftware == null
                ? null
                : new Software()
                {
                    Id = dbSoftware.Id,
                    Code = dbSoftware.Code,
                    Name = dbSoftware.Name,
                    Type = dbSoftware.NavType.Name
                };
        }

        public static DeviceAction ToModel(this DbDeviceAction dbDeviceAction)
        {
            return dbDeviceAction == null
                ? null
                : new DeviceAction()
                {
                    Id = dbDeviceAction.Id,
                    ReceiptDate = dbDeviceAction.ReceiptDate,
                    ReturnDate = dbDeviceAction.ReturnDate,
                    Type = dbDeviceAction.NavActionType.Name,
                };
        }

        public static File ToModel(this DbFile dbFile)
        {
            return dbFile == null
                ? null
                : new File()
                {
                    Id = dbFile.Id,
                    CreateDate = dbFile.CreateDate,
                    Md5Hash = dbFile.Md5Hash,
                    Type = dbFile.Type
                };
        }
    }
}