using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class KeyKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSkill_UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserSkill",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSkill",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserSkillId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSkillId",
                table: "AspNetUsers",
                column: "UserSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserSkill_UserSkillId",
                table: "AspNetUsers",
                column: "UserSkillId",
                principalTable: "UserSkill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
