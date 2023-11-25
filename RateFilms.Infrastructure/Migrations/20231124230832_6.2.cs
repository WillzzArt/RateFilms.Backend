using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Film_FilmsId",
                table: "FilmDbModelGenreDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Genre_GenreId",
                table: "FilmDbModelGenreDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Genre_GenreId",
                table: "GenreDbModelSerialDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Serial_SerialsId",
                table: "GenreDbModelSerialDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmDbModelProfessionDbModel_PersonInFilm_PersonInF~",
                table: "PersonInFilmDbModelProfessionDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmDbModelProfessionDbModel_Proffesion_ProfessionId",
                table: "PersonInFilmDbModelProfessionDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialDbModelProfessionDbModel_PersonInSerial_Perso~",
                table: "PersonInSerialDbModelProfessionDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialDbModelProfessionDbModel_Proffesion_Professio~",
                table: "PersonInSerialDbModelProfessionDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonInSerialDbModelProfessionDbModel",
                table: "PersonInSerialDbModelProfessionDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonInFilmDbModelProfessionDbModel",
                table: "PersonInFilmDbModelProfessionDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreDbModelSerialDbModel",
                table: "GenreDbModelSerialDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmDbModelGenreDbModel",
                table: "FilmDbModelGenreDbModel");

            migrationBuilder.RenameTable(
                name: "PersonInSerialDbModelProfessionDbModel",
                newName: "PersonInSerialProfession");

            migrationBuilder.RenameTable(
                name: "PersonInFilmDbModelProfessionDbModel",
                newName: "PersonInFilmProfession");

            migrationBuilder.RenameTable(
                name: "GenreDbModelSerialDbModel",
                newName: "SerialGenries");

            migrationBuilder.RenameTable(
                name: "FilmDbModelGenreDbModel",
                newName: "FilmGenries");

            migrationBuilder.RenameIndex(
                name: "IX_PersonInSerialDbModelProfessionDbModel_PersonInSerialsSeria~",
                table: "PersonInSerialProfession",
                newName: "IX_PersonInSerialProfession_PersonInSerialsSerialId_PersonInSe~");

            migrationBuilder.RenameIndex(
                name: "IX_PersonInFilmDbModelProfessionDbModel_PersonInFilmsFilmId_Pe~",
                table: "PersonInFilmProfession",
                newName: "IX_PersonInFilmProfession_PersonInFilmsFilmId_PersonInFilmsPer~");

            migrationBuilder.RenameIndex(
                name: "IX_GenreDbModelSerialDbModel_SerialsId",
                table: "SerialGenries",
                newName: "IX_SerialGenries_SerialsId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmDbModelGenreDbModel_GenreId",
                table: "FilmGenries",
                newName: "IX_FilmGenries_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonInSerialProfession",
                table: "PersonInSerialProfession",
                columns: new[] { "ProfessionId", "PersonInSerialsSerialId", "PersonInSerialsPersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonInFilmProfession",
                table: "PersonInFilmProfession",
                columns: new[] { "ProfessionId", "PersonInFilmsFilmId", "PersonInFilmsPersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerialGenries",
                table: "SerialGenries",
                columns: new[] { "GenreId", "SerialsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmGenries",
                table: "FilmGenries",
                columns: new[] { "FilmsId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenries_Film_FilmsId",
                table: "FilmGenries",
                column: "FilmsId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenries_Genre_GenreId",
                table: "FilmGenries",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_PersonInFilm_PersonInFilmsFilmId_Per~",
                table: "PersonInFilmProfession",
                columns: new[] { "PersonInFilmsFilmId", "PersonInFilmsPersonId" },
                principalTable: "PersonInFilm",
                principalColumns: new[] { "FilmId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_Proffesion_ProfessionId",
                table: "PersonInFilmProfession",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_PersonInSerial_PersonInSerialsSeri~",
                table: "PersonInSerialProfession",
                columns: new[] { "PersonInSerialsSerialId", "PersonInSerialsPersonId" },
                principalTable: "PersonInSerial",
                principalColumns: new[] { "SerialId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_Proffesion_ProfessionId",
                table: "PersonInSerialProfession",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialGenries_Genre_GenreId",
                table: "SerialGenries",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialGenries_Serial_SerialsId",
                table: "SerialGenries",
                column: "SerialsId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenries_Film_FilmsId",
                table: "FilmGenries");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenries_Genre_GenreId",
                table: "FilmGenries");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_PersonInFilm_PersonInFilmsFilmId_Per~",
                table: "PersonInFilmProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_Proffesion_ProfessionId",
                table: "PersonInFilmProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_PersonInSerial_PersonInSerialsSeri~",
                table: "PersonInSerialProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_Proffesion_ProfessionId",
                table: "PersonInSerialProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialGenries_Genre_GenreId",
                table: "SerialGenries");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialGenries_Serial_SerialsId",
                table: "SerialGenries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SerialGenries",
                table: "SerialGenries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonInSerialProfession",
                table: "PersonInSerialProfession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonInFilmProfession",
                table: "PersonInFilmProfession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmGenries",
                table: "FilmGenries");

            migrationBuilder.RenameTable(
                name: "SerialGenries",
                newName: "GenreDbModelSerialDbModel");

            migrationBuilder.RenameTable(
                name: "PersonInSerialProfession",
                newName: "PersonInSerialDbModelProfessionDbModel");

            migrationBuilder.RenameTable(
                name: "PersonInFilmProfession",
                newName: "PersonInFilmDbModelProfessionDbModel");

            migrationBuilder.RenameTable(
                name: "FilmGenries",
                newName: "FilmDbModelGenreDbModel");

            migrationBuilder.RenameIndex(
                name: "IX_SerialGenries_SerialsId",
                table: "GenreDbModelSerialDbModel",
                newName: "IX_GenreDbModelSerialDbModel_SerialsId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonInSerialProfession_PersonInSerialsSerialId_PersonInSe~",
                table: "PersonInSerialDbModelProfessionDbModel",
                newName: "IX_PersonInSerialDbModelProfessionDbModel_PersonInSerialsSeria~");

            migrationBuilder.RenameIndex(
                name: "IX_PersonInFilmProfession_PersonInFilmsFilmId_PersonInFilmsPer~",
                table: "PersonInFilmDbModelProfessionDbModel",
                newName: "IX_PersonInFilmDbModelProfessionDbModel_PersonInFilmsFilmId_Pe~");

            migrationBuilder.RenameIndex(
                name: "IX_FilmGenries_GenreId",
                table: "FilmDbModelGenreDbModel",
                newName: "IX_FilmDbModelGenreDbModel_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreDbModelSerialDbModel",
                table: "GenreDbModelSerialDbModel",
                columns: new[] { "GenreId", "SerialsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonInSerialDbModelProfessionDbModel",
                table: "PersonInSerialDbModelProfessionDbModel",
                columns: new[] { "ProfessionId", "PersonInSerialsSerialId", "PersonInSerialsPersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonInFilmDbModelProfessionDbModel",
                table: "PersonInFilmDbModelProfessionDbModel",
                columns: new[] { "ProfessionId", "PersonInFilmsFilmId", "PersonInFilmsPersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmDbModelGenreDbModel",
                table: "FilmDbModelGenreDbModel",
                columns: new[] { "FilmsId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Film_FilmsId",
                table: "FilmDbModelGenreDbModel",
                column: "FilmsId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Genre_GenreId",
                table: "FilmDbModelGenreDbModel",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Genre_GenreId",
                table: "GenreDbModelSerialDbModel",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Serial_SerialsId",
                table: "GenreDbModelSerialDbModel",
                column: "SerialsId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmDbModelProfessionDbModel_PersonInFilm_PersonInF~",
                table: "PersonInFilmDbModelProfessionDbModel",
                columns: new[] { "PersonInFilmsFilmId", "PersonInFilmsPersonId" },
                principalTable: "PersonInFilm",
                principalColumns: new[] { "FilmId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmDbModelProfessionDbModel_Proffesion_ProfessionId",
                table: "PersonInFilmDbModelProfessionDbModel",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialDbModelProfessionDbModel_PersonInSerial_Perso~",
                table: "PersonInSerialDbModelProfessionDbModel",
                columns: new[] { "PersonInSerialsSerialId", "PersonInSerialsPersonId" },
                principalTable: "PersonInSerial",
                principalColumns: new[] { "SerialId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialDbModelProfessionDbModel_Proffesion_Professio~",
                table: "PersonInSerialDbModelProfessionDbModel",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
