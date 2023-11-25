using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _63 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Proffesion_Profession",
                table: "Proffesion",
                column: "Profession",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Genre",
                table: "Genre",
                column: "Genre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proffesion_Profession",
                table: "Proffesion");

            migrationBuilder.DropIndex(
                name: "IX_Genre_Genre",
                table: "Genre");
        }
    }
}
