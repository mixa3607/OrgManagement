using System;
using WebApiSharedParts.Enums;

namespace ManagementWebApi.DataModels
{
    public class File
    {
        public long Id { get; set; }
        public string Md5Hash { get; set; }
        public DateTime? CreateDate { get; set; }
        public EFileType? Type { get; set; }
    }
}