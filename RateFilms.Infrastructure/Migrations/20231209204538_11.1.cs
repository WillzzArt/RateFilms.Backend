using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial");

            migrationBuilder.DropIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm",
                column: "CommentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial");

            migrationBuilder.DropIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm",
                column: "CommentId");
        }
    }
}
