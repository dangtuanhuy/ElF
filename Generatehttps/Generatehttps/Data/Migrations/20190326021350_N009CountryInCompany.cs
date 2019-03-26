using Microsoft.EntityFrameworkCore.Migrations;

namespace Generatehttps.Data.Migrations
{
    public partial class N009CountryInCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Company",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_CountryId",
                table: "Company",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Country_CountryId",
                table: "Company",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Country_CountryId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_CountryId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Company");
        }
    }
}
