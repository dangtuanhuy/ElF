using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Salary",
                table: "Job",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "Job",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
