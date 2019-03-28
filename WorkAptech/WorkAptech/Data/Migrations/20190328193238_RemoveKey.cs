using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class RemoveKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_ApplyDetails_AppliDetailId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_FromUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AppliDetailId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_FromUserId",
                table: "Notification");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AppliDetailId",
                table: "Notification",
                column: "AppliDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_FromUserId",
                table: "Notification",
                column: "FromUserId");

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
    }
}
