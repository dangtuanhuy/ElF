using Microsoft.EntityFrameworkCore.Migrations;

namespace Generatehttps.Data.Migrations
{
    public partial class N010UpdateJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkingForm",
                table: "Job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingTime",
                table: "Job",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingForm",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "WorkingTime",
                table: "Job");
        }
    }
}
