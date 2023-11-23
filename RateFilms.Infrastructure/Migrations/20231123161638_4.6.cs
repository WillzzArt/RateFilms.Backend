using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Films_FilmId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Seasons_SeasonId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_FilmId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeasonId",
                table: "Actors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FilmId",
                table: "Actors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ActorDbModelFilmDbModel",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDbModelFilmDbModel", x => new { x.ActorsId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_ActorDbModelFilmDbModel_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorDbModelFilmDbModel_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorDbModelSeasonDbModel",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDbModelSeasonDbModel", x => new { x.ActorsId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_ActorDbModelSeasonDbModel_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorDbModelSeasonDbModel_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorDbModelFilmDbModel_FilmId",
                table: "ActorDbModelFilmDbModel",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorDbModelSeasonDbModel_SeasonId",
                table: "ActorDbModelSeasonDbModel",
                column: "SeasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorDbModelFilmDbModel");

            migrationBuilder.DropTable(
                name: "ActorDbModelSeasonDbModel");

            migrationBuilder.AlterColumn<Guid>(
                name: "SeasonId",
                table: "Actors",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FilmId",
                table: "Actors",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_FilmId",
                table: "Actors",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Films_FilmId",
                table: "Actors",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Seasons_SeasonId",
                table: "Actors",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }
    }
}
