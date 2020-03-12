namespace ManagementWebApi.Database
{
    public class DbTaxId
    {
        public long Id { get; set; }
        public string StrSerialNumber { get; set; }

        public long TaxIdScan { get; set; }
        public DbFile NavTaxIdScan { get; set; }

        public long EmployeeId { get; set; }
        public DbEmployee NavEmployee { get; set; }

    }
}
