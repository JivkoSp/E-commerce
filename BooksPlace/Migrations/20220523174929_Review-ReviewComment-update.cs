using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksPlace.Migrations
{
    public partial class ReviewReviewCommentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ReviewComment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReviewContent",
                table: "Review",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComment_UserId",
                table: "ReviewComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComment_User",
                table: "ReviewComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComment_User",
                table: "ReviewComment");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComment_UserId",
                table: "ReviewComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ReviewComment");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewContent",
                table: "Review");
        }
    }
}
