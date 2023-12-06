using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _931 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCountSeasons",
                table: "Serial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxCountSeasons",
                table: "Serial",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
