using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Film");

            migrationBuilder.RenameColumn(
                name: "isFavorite",
                table: "FavoriteFilm",
                newName: "IsFavorite");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "FavoriteSerial",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "FavoriteFilm",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "FavoriteSerial");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "FavoriteFilm");

            migrationBuilder.RenameColumn(
                name: "IsFavorite",
                table: "FavoriteFilm",
                newName: "isFavorite");

            migrationBuilder.AddColumn<float>(
                name: "AvgRating",
                table: "Serial",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AvgRating",
                table: "Season",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AvgRating",
                table: "Film",
                type: "real",
                nullable: true);
        }
    }
}
