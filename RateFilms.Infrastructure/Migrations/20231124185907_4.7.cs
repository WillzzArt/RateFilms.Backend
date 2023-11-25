using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _47 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Serials");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Films");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Genre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmDbModelGenreDbModel",
                columns: table => new
                {
                    FilmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmDbModelGenreDbModel", x => new { x.FilmsId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_FilmDbModelGenreDbModel_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmDbModelGenreDbModel_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreDbModelSerialDbModel",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    SerialsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreDbModelSerialDbModel", x => new { x.GenreId, x.SerialsId });
                    table.ForeignKey(
                        name: "FK_GenreDbModelSerialDbModel_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreDbModelSerialDbModel_Serials_SerialsId",
                        column: x => x.SerialsId,
                        principalTable: "Serials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmDbModelGenreDbModel_GenreId",
                table: "FilmDbModelGenreDbModel",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreDbModelSerialDbModel_SerialsId",
                table: "GenreDbModelSerialDbModel",
                column: "SerialsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmDbModelGenreDbModel");

            migrationBuilder.DropTable(
                name: "GenreDbModelSerialDbModel");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Serials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
