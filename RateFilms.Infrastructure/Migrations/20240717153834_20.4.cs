using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _204 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KinopoiskId",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KinopoiskId",
                table: "Movie",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KinopoiskId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "KinopoiskId",
                table: "Movie");
        }
    }
}
