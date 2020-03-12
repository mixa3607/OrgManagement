using System;
using System.Collections.Generic;

namespace ManagementWebApi.Database
{
    public class DbFile
    {
        public long Id { get; set; }
        public string Md5Hash { get; set; }
        public DateTime CreateDate { get; set; }





        public DbCert NavCert { get; set; }
        public DbCert NavContainerCert { get; set; }
        //public List<DbPassport> NavPassports { get; set; }
        //public List<DbTaxId> NavTaxIds { get; set; }

    }
}
