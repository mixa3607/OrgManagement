﻿// <auto-generated />
using System;
using ManagementWebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementWebApi.Migrations
{
    [DbContext(typeof(ManagementDbContext))]
    [Migration("20200323102424_spltInitialsAndName")]
    partial class spltInitialsAndName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Issuer")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("NotAfter")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("NotBefore")
                        .HasColumnType("timestamp without time zone");

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

                    b.ToTable("DepartmentHelpers");
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

                    b.Property<DateTime?>("ReturnDate")
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

                    b.Property<string>("Ipv4StrAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PassportId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TaxIdId")
                        .HasColumnType("bigint");

                    b.Property<string>("WorkingPosition")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PassportId")
                        .IsUnique();

                    b.HasIndex("TaxIdId")
                        .IsUnique();

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

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

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

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("IssuedAt")
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

                    b.HasIndex("ScanFileId")
                        .IsUnique();

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

                    b.Property<string>("StrSerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TaxIdScan")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StrSerialNumber")
                        .IsUnique();

                    b.HasIndex("TaxIdScan")
                        .IsUnique();

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

            modelBuilder.Entity("ManagementWebApi.Database.DbEmployee", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbTaxId", "NavTaxId")
                        .WithOne("NavEmployee")
                        .HasForeignKey("ManagementWebApi.Database.DbEmployee", "PassportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementWebApi.Database.DbPassport", "NavPassport")
                        .WithOne("NavEmployee")
                        .HasForeignKey("ManagementWebApi.Database.DbEmployee", "TaxIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementWebApi.Database.DbPassport", b =>
                {
                    b.HasOne("ManagementWebApi.Database.DbFile", "NavScanFile")
                        .WithOne("NavPassport")
                        .HasForeignKey("ManagementWebApi.Database.DbPassport", "ScanFileId")
                        .OnDelete(DeleteBehavior.Restrict)
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
                    b.HasOne("ManagementWebApi.Database.DbFile", "NavTaxIdScan")
                        .WithOne("NavTaxId")
                        .HasForeignKey("ManagementWebApi.Database.DbTaxId", "TaxIdScan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
