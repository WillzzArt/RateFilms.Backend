using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFilm_Films_FilmId",
                table: "FavoriteFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFilm_Users_UserId",
                table: "FavoriteFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSerial_Serials_SerialId",
                table: "FavoriteSerial");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSerial_Users_UserId",
                table: "FavoriteSerial");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Films_FilmsId",
                table: "FilmDbModelGenreDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Serials_SerialsId",
                table: "GenreDbModelSerialDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Films_FilmId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Seasons_SeasonId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Serials_SerialId",
                table: "Seasons");

            migrationBuilder.DropTable(
                name: "ActorDbModelFilmDbModel");

            migrationBuilder.DropTable(
                name: "ActorDbModelSeasonDbModel");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serials",
                table: "Serials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Films",
                table: "Films");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Serials",
                newName: "Serial");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "Season");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Films",
                newName: "Film");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserName",
                table: "User",
                newName: "IX_User_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_SerialId",
                table: "Season",
                newName: "IX_Season_SerialId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_SeasonId",
                table: "Image",
                newName: "IX_Image_SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_FilmId",
                table: "Image",
                newName: "IX_Image_FilmId");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Film",
                newName: "Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serial",
                table: "Serial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season",
                table: "Season",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Film",
                table: "Film",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonInFilm",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInFilm", x => new { x.FilmId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonInFilm_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInFilm_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInSerial",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInSerial", x => new { x.SerialId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonInSerial_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInSerial_Serial_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proffesion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    PersonInFilmDbModelFilmId = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonInFilmDbModelPersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonInSerialDbModelPersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonInSerialDbModelSerialId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proffesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proffesion_PersonInFilm_PersonInFilmDbModelFilmId_PersonInF~",
                        columns: x => new { x.PersonInFilmDbModelFilmId, x.PersonInFilmDbModelPersonId },
                        principalTable: "PersonInFilm",
                        principalColumns: new[] { "FilmId", "PersonId" });
                    table.ForeignKey(
                        name: "FK_Proffesion_PersonInSerial_PersonInSerialDbModelSerialId_Per~",
                        columns: x => new { x.PersonInSerialDbModelSerialId, x.PersonInSerialDbModelPersonId },
                        principalTable: "PersonInSerial",
                        principalColumns: new[] { "SerialId", "PersonId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_ImageId",
                table: "Person",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInFilm_PersonId",
                table: "PersonInFilm",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInSerial_PersonId",
                table: "PersonInSerial",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Proffesion_PersonInFilmDbModelFilmId_PersonInFilmDbModelPer~",
                table: "Proffesion",
                columns: new[] { "PersonInFilmDbModelFilmId", "PersonInFilmDbModelPersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_Proffesion_PersonInSerialDbModelSerialId_PersonInSerialDbMo~",
                table: "Proffesion",
                columns: new[] { "PersonInSerialDbModelSerialId", "PersonInSerialDbModelPersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFilm_Film_FilmId",
                table: "FavoriteFilm",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFilm_User_UserId",
                table: "FavoriteFilm",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSerial_Serial_SerialId",
                table: "FavoriteSerial",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSerial_User_UserId",
                table: "FavoriteSerial",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Film_FilmsId",
                table: "FilmDbModelGenreDbModel",
                column: "FilmsId",
                principalTable: "Film",
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
                name: "FK_Image_Film_FilmId",
                table: "Image",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Season_SeasonId",
                table: "Image",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFilm_Film_FilmId",
                table: "FavoriteFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFilm_User_UserId",
                table: "FavoriteFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSerial_Serial_SerialId",
                table: "FavoriteSerial");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSerial_User_UserId",
                table: "FavoriteSerial");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Film_FilmsId",
                table: "FilmDbModelGenreDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Serial_SerialsId",
                table: "GenreDbModelSerialDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Film_FilmId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Season_SeasonId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season");

            migrationBuilder.DropTable(
                name: "Proffesion");

            migrationBuilder.DropTable(
                name: "PersonInFilm");

            migrationBuilder.DropTable(
                name: "PersonInSerial");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serial",
                table: "Serial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Season",
                table: "Season");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Film",
                table: "Film");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Serial",
                newName: "Serials");

            migrationBuilder.RenameTable(
                name: "Season",
                newName: "Seasons");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Film",
                newName: "Films");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserName",
                table: "Users",
                newName: "IX_Users_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Season_SerialId",
                table: "Seasons",
                newName: "IX_Seasons_SerialId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_SeasonId",
                table: "Images",
                newName: "IX_Images_SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_FilmId",
                table: "Images",
                newName: "IX_Images_FilmId");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Films",
                newName: "Autor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serials",
                table: "Serials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Films",
                table: "Films",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ImageId",
                table: "Actors",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFilm_Films_FilmId",
                table: "FavoriteFilm",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFilm_Users_UserId",
                table: "FavoriteFilm",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSerial_Serials_SerialId",
                table: "FavoriteSerial",
                column: "SerialId",
                principalTable: "Serials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSerial_Users_UserId",
                table: "FavoriteSerial",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDbModelGenreDbModel_Films_FilmsId",
                table: "FilmDbModelGenreDbModel",
                column: "FilmsId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreDbModelSerialDbModel_Serials_SerialsId",
                table: "GenreDbModelSerialDbModel",
                column: "SerialsId",
                principalTable: "Serials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Films_FilmId",
                table: "Images",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Seasons_SeasonId",
                table: "Images",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Serials_SerialId",
                table: "Seasons",
                column: "SerialId",
                principalTable: "Serials",
                principalColumn: "Id");
        }
    }
}
