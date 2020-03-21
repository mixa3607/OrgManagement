using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class addIsOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Files_ScanFileId",
                table: "Passports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedAt",
                table: "Passports",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "Passports",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "DeviceActions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Files_ScanFileId",
                table: "Passports",
                column: "ScanFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Files_ScanFileId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedAt",
                table: "Passports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "Passports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "DeviceActions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Files_ScanFileId",
                table: "Passports",
                column: "ScanFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
