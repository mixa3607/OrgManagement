using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementWebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentHelpers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentHelpers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceActionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Md5Hash = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPositionHelpers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPositionHelpers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Initials = table.Column<string>(nullable: false),
                    Batch = table.Column<long>(nullable: false),
                    SerialNumber = table.Column<long>(nullable: false),
                    Issuer = table.Column<string>(nullable: false),
                    IssuerNum = table.Column<long>(nullable: false),
                    IssuedAt = table.Column<DateTime>(nullable: false),
                    RegPlace = table.Column<string>(nullable: false),
                    BirthPlace = table.Column<string>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    ScanFileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passports_Files_ScanFileId",
                        column: x => x.ScanFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxIds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialNumber = table.Column<string>(nullable: false),
                    ScanFileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxIds", x => x.Id);
                    table.ForeignKey(
                        name: "IX_TaxIds_ScanFileIdTax",
                        column: x => x.ScanFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Department = table.Column<string>(nullable: false),
                    WorkingPosition = table.Column<string>(nullable: true),
                    Ipv4Address = table.Column<string>(nullable: false),
                    DomainNameEntry = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false),
                    PassportId = table.Column<long>(nullable: false),
                    TaxIdId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_TaxIds_PassportId",
                        column: x => x.PassportId,
                        principalTable: "TaxIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Passports_TaxIdId",
                        column: x => x.TaxIdId,
                        principalTable: "Passports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    NotBefore = table.Column<DateTime>(nullable: false),
                    NotAfter = table.Column<DateTime>(nullable: false),
                    Issuer = table.Column<string>(nullable: true),
                    CertFileId = table.Column<long>(nullable: false),
                    ContainerFileId = table.Column<long>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certs_Files_CertFileId",
                        column: x => x.CertFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certs_Files_ContainerFileId",
                        column: x => x.ContainerFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    InvNumber = table.Column<string>(nullable: true),
                    DeviceTypeId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceActions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    DeviceId = table.Column<long>(nullable: false),
                    ActionTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceActions_DeviceActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "DeviceActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceActions_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    TypeId = table.Column<long>(nullable: false),
                    DeviceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Softwares_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Softwares_SoftwareTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SoftwareTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certs_CertFileId",
                table: "Certs",
                column: "CertFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certs_ContainerFileId",
                table: "Certs",
                column: "ContainerFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certs_EmployeeId",
                table: "Certs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceActions_ActionTypeId",
                table: "DeviceActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceActions_DeviceId",
                table: "DeviceActions",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceActionTypes_Name",
                table: "DeviceActionTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_EmployeeId",
                table: "Devices",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_InvNumber",
                table: "Devices",
                column: "InvNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypes_Name",
                table: "DeviceTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PassportId",
                table: "Employees",
                column: "PassportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaxIdId",
                table: "Employees",
                column: "TaxIdId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ScanFileId",
                table: "Passports",
                column: "ScanFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_DeviceId",
                table: "Softwares",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_TypeId",
                table: "Softwares",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareTypes_Name",
                table: "SoftwareTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_ScanFileId",
                table: "TaxIds",
                column: "ScanFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_SerialNumber",
                table: "TaxIds",
                column: "SerialNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certs");

            migrationBuilder.DropTable(
                name: "DepartmentHelpers");

            migrationBuilder.DropTable(
                name: "DeviceActions");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "WorkingPositionHelpers");

            migrationBuilder.DropTable(
                name: "DeviceActionTypes");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "SoftwareTypes");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TaxIds");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
