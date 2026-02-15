using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookss.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTheLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BooksRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "BooksRating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
