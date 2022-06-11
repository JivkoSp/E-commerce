using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksPlace.Migrations
{
    public partial class ProductimageColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "Product",
                type: "image",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Product");
        }
    }
}
