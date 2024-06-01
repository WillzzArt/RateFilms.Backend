using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _181 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Token_Token",
                table: "Token",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Token_Token",
                table: "Token");
        }
    }
}
