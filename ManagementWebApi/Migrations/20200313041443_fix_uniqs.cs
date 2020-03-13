using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class fix_uniqs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Employees_EmployeeId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxIds_Employees_EmployeeId",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_TaxIds_EmployeeId",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_TaxIds_TaxIdScan",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_Passports_EmployeeId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_ScanFileId",
                table: "Passports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartamentHelpers",
                table: "DepartamentHelpers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TaxIds");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "ExpiredBefore",
                table: "Certs");

            migrationBuilder.DropColumn(
                name: "IssuedAt",
                table: "Certs");

            migrationBuilder.RenameTable(
                name: "DepartamentHelpers",
                newName: "DepartmentHelpers");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Files",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "PassportId",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxIdId",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "NotAfter",
                table: "Certs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NotBefore",
                table: "Certs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentHelpers",
                table: "DepartmentHelpers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_TaxIdScan",
                table: "TaxIds",
                column: "TaxIdScan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ScanFileId",
                table: "Passports",
                column: "ScanFileId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TaxIds_PassportId",
                table: "Employees",
                column: "PassportId",
                principalTable: "TaxIds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Passports_TaxIdId",
                table: "Employees",
                column: "TaxIdId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TaxIds_PassportId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Passports_TaxIdId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_TaxIds_TaxIdScan",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_Passports_ScanFileId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PassportId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxIdId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentHelpers",
                table: "DepartmentHelpers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TaxIdId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NotAfter",
                table: "Certs");

            migrationBuilder.DropColumn(
                name: "NotBefore",
                table: "Certs");

            migrationBuilder.RenameTable(
                name: "DepartmentHelpers",
                newName: "DepartamentHelpers");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "TaxIds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Passports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredBefore",
                table: "Certs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAt",
                table: "Certs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartamentHelpers",
                table: "DepartamentHelpers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_EmployeeId",
                table: "TaxIds",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_TaxIdScan",
                table: "TaxIds",
                column: "TaxIdScan");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_EmployeeId",
                table: "Passports",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ScanFileId",
                table: "Passports",
                column: "ScanFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Employees_EmployeeId",
                table: "Passports",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxIds_Employees_EmployeeId",
                table: "TaxIds",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
