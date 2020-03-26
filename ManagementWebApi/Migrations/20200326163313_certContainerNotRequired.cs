using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class certContainerNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certs_Files_ContainerFileId",
                table: "Certs");

            migrationBuilder.AlterColumn<long>(
                name: "ContainerFileId",
                table: "Certs",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Certs_Files_ContainerFileId",
                table: "Certs",
                column: "ContainerFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certs_Files_ContainerFileId",
                table: "Certs");

            migrationBuilder.AlterColumn<long>(
                name: "ContainerFileId",
                table: "Certs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Certs_Files_ContainerFileId",
                table: "Certs",
                column: "ContainerFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
