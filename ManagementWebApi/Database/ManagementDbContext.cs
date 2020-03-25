using Microsoft.EntityFrameworkCore;

namespace ManagementWebApi.Database
{
    public class ManagementDbContext : DbContext
    {
        public DbSet<DbCert> Certs { get; set; }
        public DbSet<DbDepartamentHelper> DepartmentHelpers { get; set; }
        public DbSet<DbDevice> Devices { get; set; }
        public DbSet<DbDeviceAction> DeviceActions { get; set; }
        public DbSet<DbDeviceActionType> DeviceActionTypes { get; set; }
        public DbSet<DbDeviceType> DeviceTypes { get; set; }
        public DbSet<DbEmployee> Employees { get; set; }
        public DbSet<DbFile> Files { get; set; }
        public DbSet<DbPassport> Passports { get; set; }
        public DbSet<DbSoftware> Softwares { get; set; }
        public DbSet<DbSoftwareType> SoftwareTypes { get; set; }
        public DbSet<DbTaxId> TaxIds { get; set; }
        public DbSet<DbWorkingPositionHelper> WorkingPositionHelpers { get; set; }

        public ManagementDbContext(DbContextOptions<ManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var certs = modelBuilder.Entity<DbCert>();
            var departmentHelpers = modelBuilder.Entity<DbDepartamentHelper>();
            var devices = modelBuilder.Entity<DbDevice>();
            var deviceActions = modelBuilder.Entity<DbDeviceAction>();
            var deviceActionTypes = modelBuilder.Entity<DbDeviceActionType>();
            var deviceTypes = modelBuilder.Entity<DbDeviceType>();
            var employees = modelBuilder.Entity<DbEmployee>();
            var files = modelBuilder.Entity<DbFile>();
            var passports = modelBuilder.Entity<DbPassport>();
            var softwares = modelBuilder.Entity<DbSoftware>();
            var softwareTypes = modelBuilder.Entity<DbSoftwareType>();
            var taxIds = modelBuilder.Entity<DbTaxId>();
            var workingPositionHelpers = modelBuilder.Entity<DbWorkingPositionHelper>();

            certs.HasKey(x => x.Id);
            certs.Property(x => x.Id).IsRequired();
            certs.Property(x => x.Name).IsRequired();
            certs.HasIndex(x => x.ContainerFileId).IsUnique();
            certs.HasIndex(x => x.CertFileId).IsUnique();
            certs.HasOne(x => x.NavCertFile).WithOne(y=>y.NavCert).HasForeignKey<DbCert>(x => x.CertFileId);
            certs.HasOne(x => x.NavContainerFile).WithOne(y=>y.NavContainerCert).HasForeignKey<DbCert>(x => x.ContainerFileId);
            certs.HasOne(x => x.NavEmployee).WithMany(y => y.NavCerts).HasForeignKey(x => x.EmployeeId);

            devices.HasKey(x => x.Id);
            devices.HasIndex(x => x.InvNumber);
            devices.Property(x => x.InvNumber).IsRequired(false);
            devices.Property(x => x.Id).IsRequired();
            devices.Property(x => x.Name).IsRequired();
            devices.HasOne(x => x.NavDeviceType).WithMany(y => y.NavDevices).HasForeignKey(x => x.DeviceTypeId).OnDelete(DeleteBehavior.Restrict); ;
            devices.HasOne(x => x.NavEmployee).WithMany(y => y.NavDevices).HasForeignKey(x => x.EmployeeId);

            deviceActions.HasKey(x => x.Id);
            deviceActions.HasOne(x => x.NavActionType).WithMany(y => y.NavDeviceActions).HasForeignKey(x => x.ActionTypeId).OnDelete(DeleteBehavior.Restrict); ;
            deviceActions.HasOne(x => x.NavDevice).WithMany(y => y.NavActions).HasForeignKey(x => x.DeviceId);

            deviceActionTypes.HasKey(x => x.Id);
            deviceActionTypes.HasIndex(x => x.Name).IsUnique();
            deviceActionTypes.Property(x => x.Name).IsRequired();

            deviceTypes.HasKey(x => x.Id);
            deviceTypes.HasIndex(x => x.Name).IsUnique();
            deviceTypes.Property(x => x.Name).IsRequired();

            employees.HasKey(x => x.Id);
            employees.Property(x => x.Name).IsRequired();
            employees.Property(x => x.Ipv4StrAddress).IsRequired();
            employees.Property(x => x.Department).IsRequired();
            employees.Property(x => x.DomainNameEntry).IsRequired();
            employees.Property(x => x.PhoneNumber).IsRequired();
            employees.Property(x => x.Email).IsRequired();
            employees.HasOne(x => x.NavPassport).WithOne(y => y.NavEmployee).HasForeignKey<DbEmployee>(z => z.TaxIdId);
            employees.HasOne(x => x.NavTaxId).WithOne(y => y.NavEmployee).HasForeignKey<DbEmployee>(y => y.PassportId);

            files.HasKey(x => x.Id);
            files.Property(x => x.Md5Hash).IsRequired();

            passports.HasKey(x => x.Id);
            passports.Property(x => x.Initials).IsRequired();
            passports.Property(x => x.Issuer).IsRequired();
            passports.Property(x => x.RegPlace).IsRequired();
            passports.Property(x => x.BirthPlace).IsRequired();
            //passports.HasIndex(x => x.EmployeeId).IsUnique();
            passports.HasIndex(x => x.ScanFileId).IsUnique();
            passports.HasOne(x => x.NavScanFile).WithOne(y=>y.NavPassport).HasForeignKey<DbPassport>(x => x.ScanFileId).OnDelete(DeleteBehavior.Restrict);

            softwares.HasKey(x => x.Id);
            softwares.Property(x => x.Code).IsRequired();
            softwares.Property(x => x.Name).IsRequired();
            softwares.HasOne(x => x.NavDevice).WithMany(y => y.NavSoftwares).HasForeignKey(x => x.DeviceId);
            softwares.HasOne(x => x.NavType).WithMany(y => y.NavSoftwares).HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);

            softwareTypes.HasKey(x => x.Id);
            softwareTypes.HasIndex(x => x.Name).IsUnique();
            softwareTypes.Property(x => x.Name).IsRequired();
            
            taxIds.HasKey(x => x.Id);
            taxIds.HasIndex(x => x.StrSerialNumber).IsUnique();
            taxIds.Property(x => x.StrSerialNumber).IsRequired();
            taxIds.HasOne(x => x.NavTaxIdScan).WithOne(y=>y.NavTaxId).HasForeignKey<DbTaxId>(x => x.TaxIdScan);

        }
    }
}
