using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksPlace.Migrations
{
    public partial class ReviewCommentUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ReviewComment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ReviewComment");
        }
    }
}
