using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbSoftwareType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<DbSoftware> NavSoftwares { get; set; }
    }
}
