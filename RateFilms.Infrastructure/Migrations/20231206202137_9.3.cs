using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _93 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxCountSeasons",
                table: "Serial",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountMaxSeries",
                table: "Season",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCountSeasons",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "CountMaxSeries",
                table: "Season");
        }
    }
}
