using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbSoftware
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long TypeId { get; set; }
        public DbSoftwareType NavType { get; set; }

        public long DeviceId { get; set; }
        public DbDevice NavDevice { get; set; }
    }
}
