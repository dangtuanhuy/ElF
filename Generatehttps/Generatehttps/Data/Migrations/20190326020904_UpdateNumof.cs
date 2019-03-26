using Microsoft.EntityFrameworkCore.Migrations;

namespace Generatehttps.Data.Migrations
{
    public partial class UpdateNumof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Company",
                newName: "NumberOfEmployee");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployeeId",
                table: "Company",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_NumberOfEmployeeId",
                table: "Company",
                column: "NumberOfEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_NumOfEmployee_NumberOfEmployeeId",
                table: "Company",
                column: "NumberOfEmployeeId",
                principalTable: "NumOfEmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_NumOfEmployee_NumberOfEmployeeId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_NumberOfEmployeeId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployeeId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "NumberOfEmployee",
                table: "Company",
                newName: "Address");
        }
    }
}
