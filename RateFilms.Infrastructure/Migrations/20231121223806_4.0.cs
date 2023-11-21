using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmGenre");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Films",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Films",
                newName: "Autor");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Seasons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<float>(
                name: "AvgRating",
                table: "Seasons",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<Guid>(
                name: "SerialDbModelId",
                table: "Images",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Films",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<float>(
                name: "AgeRating",
                table: "Films",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "SerialDbModelId",
                table: "Actors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_SerialDbModelId",
                table: "Images",
                column: "SerialDbModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SerialDbModelId",
                table: "Actors",
                column: "SerialDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Serials_SerialDbModelId",
                table: "Actors",
                column: "SerialDbModelId",
                principalTable: "Serials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Serials_SerialDbModelId",
                table: "Images",
                column: "SerialDbModelId",
                principalTable: "Serials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Serials_SerialDbModelId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Serials_SerialDbModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SerialDbModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Actors_SerialDbModelId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "SerialDbModelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SerialDbModelId",
                table: "Actors");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Films",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Films",
                newName: "Author");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Seasons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AvgRating",
                table: "Seasons",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeRating",
                table: "Films",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenre",
                columns: table => new
                {
                    FilmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenre", x => new { x.FilmsId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_FilmGenre_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_GenreId",
                table: "FilmGenre",
                column: "GenreId");
        }
    }
}
