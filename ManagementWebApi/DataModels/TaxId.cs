namespace ManagementWebApi.DataModels
{
    public class TaxId
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }

        //public long TaxIdScanFileId { get; set; }
        public File TaxIdScan { get; set; }
    }
}