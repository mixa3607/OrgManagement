using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbDevice
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InvNumber { get; set; }

        public List<DbDeviceAction> NavActions { get; set; }

        public long DeviceTypeId { get; set; }
        public DbDeviceType NavDeviceType { get; set; }

        public long EmployeeId { get; set; }
        public DbEmployee NavEmployee { get; set; }

        public List<DbSoftware> NavSoftwares { get; set; }
    }
}
