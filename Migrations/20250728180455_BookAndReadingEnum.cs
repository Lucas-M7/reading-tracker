using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class BookAndReadingEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Readings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Readings");
        }
    }
}
