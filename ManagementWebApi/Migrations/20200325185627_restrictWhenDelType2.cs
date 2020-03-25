using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class restrictWhenDelType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceActions_DeviceActionTypes_ActionTypeId",
                table: "DeviceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_SoftwareTypes_TypeId",
                table: "Softwares");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceActions_DeviceActionTypes_ActionTypeId",
                table: "DeviceActions",
                column: "ActionTypeId",
                principalTable: "DeviceActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_SoftwareTypes_TypeId",
                table: "Softwares",
                column: "TypeId",
                principalTable: "SoftwareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceActions_DeviceActionTypes_ActionTypeId",
                table: "DeviceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_SoftwareTypes_TypeId",
                table: "Softwares");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceActions_DeviceActionTypes_ActionTypeId",
                table: "DeviceActions",
                column: "ActionTypeId",
                principalTable: "DeviceActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_SoftwareTypes_TypeId",
                table: "Softwares",
                column: "TypeId",
                principalTable: "SoftwareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
