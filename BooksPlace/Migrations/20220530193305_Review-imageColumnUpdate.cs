using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksPlace.Migrations
{
    public partial class ReviewimageColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ReviewImage",
                table: "Review",
                type: "image",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewImage",
                table: "Review");
        }
    }
}
