using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proffesion_PersonInFilm_PersonInFilmDbModelFilmId_PersonInF~",
                table: "Proffesion");

            migrationBuilder.DropForeignKey(
                name: "FK_Proffesion_PersonInSerial_PersonInSerialDbModelSerialId_Per~",
                table: "Proffesion");

            migrationBuilder.DropIndex(
                name: "IX_Proffesion_PersonInFilmDbModelFilmId_PersonInFilmDbModelPer~",
                table: "Proffesion");

            migrationBuilder.DropIndex(
                name: "IX_Proffesion_PersonInSerialDbModelSerialId_PersonInSerialDbMo~",
                table: "Proffesion");

            migrationBuilder.DropColumn(
                name: "PersonInFilmDbModelFilmId",
                table: "Proffesion");

            migrationBuilder.DropColumn(
                name: "PersonInFilmDbModelPersonId",
                table: "Proffesion");

            migrationBuilder.DropColumn(
                name: "PersonInSerialDbModelPersonId",
                table: "Proffesion");

            migrationBuilder.DropColumn(
                name: "PersonInSerialDbModelSerialId",
                table: "Proffesion");

            migrationBuilder.CreateTable(
                name: "PersonInFilmDbModelProfessionDbModel",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "integer", nullable: false),
                    PersonInFilmsFilmId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonInFilmsPersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInFilmDbModelProfessionDbModel", x => new { x.ProfessionId, x.PersonInFilmsFilmId, x.PersonInFilmsPersonId });
                    table.ForeignKey(
                        name: "FK_PersonInFilmDbModelProfessionDbModel_PersonInFilm_PersonInF~",
                        columns: x => new { x.PersonInFilmsFilmId, x.PersonInFilmsPersonId },
                        principalTable: "PersonInFilm",
                        principalColumns: new[] { "FilmId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInFilmDbModelProfessionDbModel_Proffesion_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Proffesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInSerialDbModelProfessionDbModel",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "integer", nullable: false),
                    PersonInSerialsSerialId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonInSerialsPersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInSerialDbModelProfessionDbModel", x => new { x.ProfessionId, x.PersonInSerialsSerialId, x.PersonInSerialsPersonId });
                    table.ForeignKey(
                        name: "FK_PersonInSerialDbModelProfessionDbModel_PersonInSerial_Perso~",
                        columns: x => new { x.PersonInSerialsSerialId, x.PersonInSerialsPersonId },
                        principalTable: "PersonInSerial",
                        principalColumns: new[] { "SerialId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInSerialDbModelProfessionDbModel_Proffesion_Professio~",
                        column: x => x.ProfessionId,
                        principalTable: "Proffesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonInFilmDbModelProfessionDbModel_PersonInFilmsFilmId_Pe~",
                table: "PersonInFilmDbModelProfessionDbModel",
                columns: new[] { "PersonInFilmsFilmId", "PersonInFilmsPersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonInSerialDbModelProfessionDbModel_PersonInSerialsSeria~",
                table: "PersonInSerialDbModelProfessionDbModel",
                columns: new[] { "PersonInSerialsSerialId", "PersonInSerialsPersonId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonInFilmDbModelProfessionDbModel");

            migrationBuilder.DropTable(
                name: "PersonInSerialDbModelProfessionDbModel");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonInFilmDbModelFilmId",
                table: "Proffesion",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonInFilmDbModelPersonId",
                table: "Proffesion",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonInSerialDbModelPersonId",
                table: "Proffesion",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonInSerialDbModelSerialId",
                table: "Proffesion",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proffesion_PersonInFilmDbModelFilmId_PersonInFilmDbModelPer~",
                table: "Proffesion",
                columns: new[] { "PersonInFilmDbModelFilmId", "PersonInFilmDbModelPersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_Proffesion_PersonInSerialDbModelSerialId_PersonInSerialDbMo~",
                table: "Proffesion",
                columns: new[] { "PersonInSerialDbModelSerialId", "PersonInSerialDbModelPersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Proffesion_PersonInFilm_PersonInFilmDbModelFilmId_PersonInF~",
                table: "Proffesion",
                columns: new[] { "PersonInFilmDbModelFilmId", "PersonInFilmDbModelPersonId" },
                principalTable: "PersonInFilm",
                principalColumns: new[] { "FilmId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Proffesion_PersonInSerial_PersonInSerialDbModelSerialId_Per~",
                table: "Proffesion",
                columns: new[] { "PersonInSerialDbModelSerialId", "PersonInSerialDbModelPersonId" },
                principalTable: "PersonInSerial",
                principalColumns: new[] { "SerialId", "PersonId" });
        }
    }
}
