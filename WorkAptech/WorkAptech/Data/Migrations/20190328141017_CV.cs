using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class CV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "ApplyDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "ApplyDetails");
        }
    }
}
