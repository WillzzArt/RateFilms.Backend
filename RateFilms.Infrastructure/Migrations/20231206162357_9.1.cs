using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _91 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentInFilm_Film_FilmDbModelId",
                table: "CommentInFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInSerial_Serial_SerialDbModelId",
                table: "CommentInSerial");

            migrationBuilder.DropIndex(
                name: "IX_CommentInSerial_SerialDbModelId",
                table: "CommentInSerial");

            migrationBuilder.DropIndex(
                name: "IX_CommentInFilm_FilmDbModelId",
                table: "CommentInFilm");

            migrationBuilder.DropColumn(
                name: "SerialDbModelId",
                table: "CommentInSerial");

            migrationBuilder.DropColumn(
                name: "FilmDbModelId",
                table: "CommentInFilm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SerialDbModelId",
                table: "CommentInSerial",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FilmDbModelId",
                table: "CommentInFilm",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_SerialDbModelId",
                table: "CommentInSerial",
                column: "SerialDbModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_FilmDbModelId",
                table: "CommentInFilm",
                column: "FilmDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInFilm_Film_FilmDbModelId",
                table: "CommentInFilm",
                column: "FilmDbModelId",
                principalTable: "Film",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInSerial_Serial_SerialDbModelId",
                table: "CommentInSerial",
                column: "SerialDbModelId",
                principalTable: "Serial",
                principalColumn: "Id");
        }
    }
}
