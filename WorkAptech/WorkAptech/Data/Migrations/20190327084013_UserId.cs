﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAptech.Data.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Job",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_UserId",
                table: "Job",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_UserId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Job");
        }
    }
}
