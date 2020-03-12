using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementWebApi.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certs_Files_DbFileId",
                table: "Certs");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Files_DbFileId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxIds_Files_DbFileId",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_TaxIds_DbFileId",
                table: "TaxIds");

            migrationBuilder.DropIndex(
                name: "IX_Passports_DbFileId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Certs_DbFileId",
                table: "Certs");

            migrationBuilder.DropColumn(
                name: "DbFileId",
                table: "TaxIds");

            migrationBuilder.DropColumn(
                name: "DbFileId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "DbFileId",
                table: "Certs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DbFileId",
                table: "TaxIds",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbFileId",
                table: "Passports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbFileId",
                table: "Certs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxIds_DbFileId",
                table: "TaxIds",
                column: "DbFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_DbFileId",
                table: "Passports",
                column: "DbFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Certs_DbFileId",
                table: "Certs",
                column: "DbFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certs_Files_DbFileId",
                table: "Certs",
                column: "DbFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Files_DbFileId",
                table: "Passports",
                column: "DbFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxIds_Files_DbFileId",
                table: "TaxIds",
                column: "DbFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
