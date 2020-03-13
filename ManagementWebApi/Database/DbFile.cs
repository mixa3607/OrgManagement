using System;
using System.Collections.Generic;
using WebApiSharedParts.Enums;

namespace ManagementWebApi.Database
{
    public class DbFile
    {
        public long Id { get; set; }
        public string Md5Hash { get; set; }
        public DateTime CreateDate { get; set; }
        public EFileType Type { get; set; } = EFileType.Binary;

        public DbCert NavCert { get; set; }
        public DbCert NavContainerCert { get; set; }
        public DbPassport NavPassport { get; set; }
        public DbTaxId NavTaxId { get; set; }

    }
}
