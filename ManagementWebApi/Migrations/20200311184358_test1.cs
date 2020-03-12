using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certs_CertFileId",
                table: "Certs");

            migrationBuilder.DropIndex(
                name: "IX_Certs_ContainerFileId",
                table: "Certs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certs_CertFileId",
                table: "Certs");

            migrationBuilder.DropIndex(
                name: "IX_Certs_ContainerFileId",
                table: "Certs");

            migrationBuilder.CreateIndex(
                name: "IX_Certs_CertFileId",
                table: "Certs",
                column: "CertFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Certs_ContainerFileId",
                table: "Certs",
                column: "ContainerFileId");
        }
    }
}
