using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbDeviceActionType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<DbDeviceAction> NavDeviceActions { get; set; }

    }
}
