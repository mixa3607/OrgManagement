namespace ManagementWebApi.Database
{
    public class DbTaxId
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }

        public long ScanFileId { get; set; }
        public DbFile NavScanFileId { get; set; }

        public DbEmployee NavEmployee { get; set; }

    }
}
