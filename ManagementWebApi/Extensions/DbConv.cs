using System.Linq;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;

namespace ManagementWebApi.Extensions
{
    public static class DbConv
    {
        public static Employee ToModel(this DbEmployee dbEmployee, bool noCerts = false, bool noDevices = false)
        {
            return dbEmployee == null
                ? null
                : new Employee()
                {
                    Id = dbEmployee.Id,
                    Name = dbEmployee.Name,
                    Department = dbEmployee.Department,
                    DomainNameEntry = dbEmployee.DomainNameEntry,
                    Email = dbEmployee.Email,
                    WorkingPosition = dbEmployee.WorkingPosition,
                    PhoneNumber = dbEmployee.PhoneNumber,
                    Ipv4Address = dbEmployee.Ipv4StrAddress,
                    IsOnline = dbEmployee.IsOnline,
                    Passport = dbEmployee.NavPassport.ToModel(),
                    TaxId = dbEmployee.NavTaxId.ToModel(),
                    Certs = noCerts ? null : dbEmployee.NavCerts?.Select(x => x.ToModel()),
                    Devices = noDevices ? null : dbEmployee.NavDevices?.Select(x => x.ToModel())
                };
        }

        public static Passport ToModel(this DbPassport dbPassport)
        {
            return dbPassport == null
                ? null
                : new Passport()
                {
                    Id = dbPassport.Id,
                    Initials = dbPassport.Initials,
                    Batch = dbPassport.Batch,
                    Issuer = dbPassport.Issuer,
                    BirthPlace = dbPassport.BirthPlace,
                    BirthDay = dbPassport.BirthDay,
                    IssuedAt = dbPassport.IssuedAt,
                    IssuerNum = dbPassport.IssuerNum,
                    RegPlace = dbPassport.RegPlace,
                    SerialNumber = dbPassport.SerialNumber,
                    ScanFileId = dbPassport.ScanFileId,
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
                    ScanFileId = dbTax.TaxIdScan,
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
                    CertFileId = dbCert.CertFileId,
                    ContainerFileId = dbCert.ContainerFileId,
                    Employee = dbCert.NavEmployee.ToModel(true, true)
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
                    TypeId = dbDevice.DeviceTypeId,
                    Actions = dbDevice.NavActions?.Select(x => x.ToModel()),
                    Softwares = dbDevice.NavSoftwares?.Select(x => x.ToModel()),
                    Employee = dbDevice.NavEmployee.ToModel(true, true)
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
                    TypeId = dbDeviceAction.ActionTypeId,
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

        public static DeviceType ToModel(this DbDeviceType dbDeviceType)
        {
            return dbDeviceType == null ? null : new DeviceType() { Id = dbDeviceType.Id, Name = dbDeviceType.Name };
        }
        public static DeviceActionType ToModel(this DbDeviceActionType dbDeviceActionType)
        {
            return dbDeviceActionType == null ? null : new DeviceActionType() { Id = dbDeviceActionType.Id, Name = dbDeviceActionType.Name };
        }
    }
}