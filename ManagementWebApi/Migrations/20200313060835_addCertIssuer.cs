using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class addCertIssuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "Certs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "Certs");
        }
    }
}
