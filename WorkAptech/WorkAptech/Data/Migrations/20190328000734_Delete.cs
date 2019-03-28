using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillJob",
                table: "SkillJob");

            migrationBuilder.DropIndex(
                name: "IX_SkillJob_JobId",
                table: "SkillJob");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SkillJob_Id",
                table: "SkillJob",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillJob",
                table: "SkillJob",
                columns: new[] { "JobId", "SkillId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SkillJob_Id",
                table: "SkillJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillJob",
                table: "SkillJob");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillJob",
                table: "SkillJob",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SkillJob_JobId",
                table: "SkillJob",
                column: "JobId");
        }
    }
}
