﻿// <auto-generated />
using System;
using ManagementWebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementWebApi.Migrations
{
    [DbContext(typeof(ManagementDbContext))]
    partial class ManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ManagementWebApi.Database.DbCert", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CertFileId")
                        .HasColumnType("bigint");

                    b.Property<long>("ContainerFileId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ExpiredBefore")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CertFileId")
                        .IsUnique();

                    b.HasIndex("ContainerFileId")
                        .IsUnique();

                    b.HasIndex("EmployeeId");

                    b.ToTable("Certs");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDepartamentHelper", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DepartamentHelpers");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("DeviceTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("InvNumber")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InvNumber");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDeviceAction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ActionTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceActions");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDeviceActionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("DeviceActionTypes");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDeviceType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbEmployee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DomainNameEntry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ipv4StrAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkingPosition")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Md5Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbPassport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("Batch")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("IssuerNum")
                        .HasColumnType("bigint");

                    b.Property<string>("RegPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ScanFileId")
                        .HasColumnType("bigint");

                    b.Property<long>("SerialNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("ScanFileId");

                    b.ToTable("Passports");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbSoftware", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("TypeId");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbSoftwareType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SoftwareTypes");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbTaxId", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("StrSerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TaxIdScan")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("StrSerialNumber")
                        .IsUnique();

                    b.HasIndex("TaxIdScan");

                    b.ToTable("TaxIds");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbWorkingPositionHelper", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkingPositionHelpers");
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbCert", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbFile", "NavCertFile")
                        .WithOne("NavCert")
                        .HasForeignKey("ManagementWebApi.Database.DbCert", "CertFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbFile", "NavContainerFile")
                        .WithOne("NavContainerCert")
                        .HasForeignKey("ManagementWebApi.Database.DbCert", "ContainerFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbEmployee", "NavEmployee")
                        .WithMany("NavCerts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDevice", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbDeviceType", "NavDeviceType")
                        .WithMany("NavDevices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbEmployee", "NavEmployee")
                        .WithMany("NavDevices")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbDeviceAction", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbDeviceActionType", "NavActionType")
                        .WithMany("NavDeviceActions")
                        .HasForeignKey("ActionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbDevice", "NavDevice")
                        .WithMany("NavActions")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbPassport", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbEmployee", "NavEmployee")
                        .WithOne("NavPassport")
                        .HasForeignKey("ManagementWebApi.Database.DbPassport", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbFile", "NavScanFile")
                        .WithMany()
                        .HasForeignKey("ScanFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbSoftware", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbDevice", "NavDevice")
                        .WithMany("NavSoftwares")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbSoftwareType", "NavType")
                        .WithMany("NavSoftwares")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbTaxId", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbEmployee", "NavEmployee")
                        .WithOne("NavTaxId")
                        .HasForeignKey("ManagementWebApi.Database.DbTaxId", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbFile", "NavTaxIdScan")
                        .WithMany()
                        .HasForeignKey("TaxIdScan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
