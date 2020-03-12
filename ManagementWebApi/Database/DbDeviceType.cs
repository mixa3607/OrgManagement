using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbDeviceType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<DbDevice> NavDevices { get; set; }
    }
}
