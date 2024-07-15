using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _200 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Film_FilmId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season");

            migrationBuilder.DropForeignKey(
                name: "FK_Serial_Image_PreviewImageId",
                table: "Serial");

            migrationBuilder.DropTable(
                name: "CommentInFilm");

            migrationBuilder.DropTable(
                name: "CommentInSerial");

            migrationBuilder.DropTable(
                name: "FilmGenries");

            migrationBuilder.DropTable(
                name: "PersonInFilmProfession");

            migrationBuilder.DropTable(
                name: "PersonInSerialProfession");

            migrationBuilder.DropTable(
                name: "SerialGenries");

            migrationBuilder.DropTable(
                name: "FavoriteFilm");

            migrationBuilder.DropTable(
                name: "FavoriteSerial");

            migrationBuilder.DropTable(
                name: "PersonInFilm");

            migrationBuilder.DropTable(
                name: "PersonInSerial");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serial",
                table: "Serial");

            migrationBuilder.RenameTable(
                name: "Serial",
                newName: "Movie");

            migrationBuilder.RenameColumn(
                name: "RealeseDate",
                table: "Movie",
                newName: "ReleaseDate");

            migrationBuilder.RenameIndex(
                name: "IX_Serial_PreviewImageId",
                table: "Movie",
                newName: "IX_Movie_PreviewImageId");

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                table: "Comment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteMovieId",
                table: "Comment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteUserId",
                table: "Comment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Movie",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Movie",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "Movie",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoriteMovie",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMovie", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMovie_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.GenreId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInMovie",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInMovie", x => new { x.MovieId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonInMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInMovie_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInMovieProfession",
                columns: table => new
                {
                    ProfessionsId = table.Column<int>(type: "integer", nullable: false),
                    PersonInMoviesMovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonInMoviesPersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInMovieProfession", x => new { x.ProfessionsId, x.PersonInMoviesMovieId, x.PersonInMoviesPersonId });
                    table.ForeignKey(
                        name: "FK_PersonInMovieProfession_PersonInMovie_PersonInMoviesMovieId~",
                        columns: x => new { x.PersonInMoviesMovieId, x.PersonInMoviesPersonId },
                        principalTable: "PersonInMovie",
                        principalColumns: new[] { "MovieId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInMovieProfession_Profession_ProfessionsId",
                        column: x => x.ProfessionsId,
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FavoriteMovieId_FavoriteUserId",
                table: "Comment",
                columns: new[] { "FavoriteMovieId", "FavoriteUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovie_UserId",
                table: "FavoriteMovie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MoviesId",
                table: "MovieGenres",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInMovie_PersonId",
                table: "PersonInMovie",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInMovieProfession_PersonInMoviesMovieId_PersonInMovie~",
                table: "PersonInMovieProfession",
                columns: new[] { "PersonInMoviesMovieId", "PersonInMoviesPersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteMovieId_FavoriteUserId",
                table: "Comment",
                columns: new[] { "FavoriteMovieId", "FavoriteUserId" },
                principalTable: "FavoriteMovie",
                principalColumns: new[] { "MovieId", "UserId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Movie_FilmId",
                table: "Image",
                column: "FilmId",
                principalTable: "Movie",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Image_PreviewImageId",
                table: "Movie",
                column: "PreviewImageId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Movie_SerialId",
                table: "Season",
                column: "SerialId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteMovieId_FavoriteUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Movie_FilmId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Image_PreviewImageId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Season_Movie_SerialId",
                table: "Season");

            migrationBuilder.DropTable(
                name: "FavoriteMovie");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "PersonInMovieProfession");

            migrationBuilder.DropTable(
                name: "PersonInMovie");

            migrationBuilder.DropIndex(
                name: "IX_Comment_FavoriteMovieId_FavoriteUserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "FavoriteMovieId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "FavoriteUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Serial");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Serial",
                newName: "RealeseDate");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_PreviewImageId",
                table: "Serial",
                newName: "IX_Serial_PreviewImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serial",
                table: "Serial",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoriteSerial",
                columns: table => new
                {
                    SerialId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteSerial", x => new { x.SerialId, x.UserId });
                    table.UniqueConstraint("AK_FavoriteSerial_FavoriteId", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_FavoriteSerial_Serial_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteSerial_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgeRating = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonInSerial",
                columns: table => new
                {
                    SerialId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "SerialGenries",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    SerialsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialGenries", x => new { x.GenreId, x.SerialsId });
                    table.ForeignKey(
                        name: "FK_SerialGenries_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SerialGenries_Serial_SerialsId",
                        column: x => x.SerialsId,
                        principalTable: "Serial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentInSerial",
                columns: table => new
                {
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentInSerial", x => new { x.FavoriteId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_CommentInSerial_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentInSerial_FavoriteSerial_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "FavoriteSerial",
                        principalColumn: "FavoriteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteFilm",
                columns: table => new
                {
                    FilmId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteFilm", x => new { x.FilmId, x.UserId });
                    table.UniqueConstraint("AK_FavoriteFilm_FavoriteId", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_FavoriteFilm_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteFilm_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenries",
                columns: table => new
                {
                    FilmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenries", x => new { x.FilmsId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_FilmGenries_Film_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenries_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInFilm",
                columns: table => new
                {
                    FilmId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "PersonInSerialProfession",
                columns: table => new
                {
                    ProfessionsId = table.Column<int>(type: "integer", nullable: false),
                    PersonInSerialsSerialId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonInSerialsPersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInSerialProfession", x => new { x.ProfessionsId, x.PersonInSerialsSerialId, x.PersonInSerialsPersonId });
                    table.ForeignKey(
                        name: "FK_PersonInSerialProfession_PersonInSerial_PersonInSerialsSeri~",
                        columns: x => new { x.PersonInSerialsSerialId, x.PersonInSerialsPersonId },
                        principalTable: "PersonInSerial",
                        principalColumns: new[] { "SerialId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInSerialProfession_Profession_ProfessionsId",
                        column: x => x.ProfessionsId,
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentInFilm",
                columns: table => new
                {
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentInFilm", x => new { x.FavoriteId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_CommentInFilm_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentInFilm_FavoriteFilm_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "FavoriteFilm",
                        principalColumn: "FavoriteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInFilmProfession",
                columns: table => new
                {
                    ProfessionsId = table.Column<int>(type: "integer", nullable: false),
                    PersonInFilmsFilmId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonInFilmsPersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInFilmProfession", x => new { x.ProfessionsId, x.PersonInFilmsFilmId, x.PersonInFilmsPersonId });
                    table.ForeignKey(
                        name: "FK_PersonInFilmProfession_PersonInFilm_PersonInFilmsFilmId_Per~",
                        columns: x => new { x.PersonInFilmsFilmId, x.PersonInFilmsPersonId },
                        principalTable: "PersonInFilm",
                        principalColumns: new[] { "FilmId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInFilmProfession_Profession_ProfessionsId",
                        column: x => x.ProfessionsId,
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFilm_UserId",
                table: "FavoriteFilm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSerial_UserId",
                table: "FavoriteSerial",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenries_GenreId",
                table: "FilmGenries",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInFilm_PersonId",
                table: "PersonInFilm",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInFilmProfession_PersonInFilmsFilmId_PersonInFilmsPer~",
                table: "PersonInFilmProfession",
                columns: new[] { "PersonInFilmsFilmId", "PersonInFilmsPersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonInSerial_PersonId",
                table: "PersonInSerial",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInSerialProfession_PersonInSerialsSerialId_PersonInSe~",
                table: "PersonInSerialProfession",
                columns: new[] { "PersonInSerialsSerialId", "PersonInSerialsPersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_SerialGenries_SerialsId",
                table: "SerialGenries",
                column: "SerialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Film_FilmId",
                table: "Image",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Serial_Image_PreviewImageId",
                table: "Serial",
                column: "PreviewImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }
    }
}
