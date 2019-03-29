using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class HJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSkill",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
                name: "IX_Notification_AppliDetailId",
                table: "Notification",
                column: "AppliDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_FromUserId",
                table: "Notification",
                column: "FromUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_ApplyDetails_AppliDetailId",
                table: "Notification",
                column: "AppliDetailId",
                principalTable: "ApplyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_FromUserId",
                table: "Notification",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSkill_UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_ApplyDetails_AppliDetailId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_FromUserId",
                table: "Notification");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AppliDetailId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_FromUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSkillId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSkill",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
