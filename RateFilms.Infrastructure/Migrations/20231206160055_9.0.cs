using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                table: "FavoriteSerial",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                table: "FavoriteFilm",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FavoriteSerial_FavoriteId",
                table: "FavoriteSerial",
                column: "FavoriteId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FavoriteFilm_FavoriteId",
                table: "FavoriteFilm",
                column: "FavoriteId");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsEdit = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentInFilm",
                columns: table => new
                {
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmDbModelId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_CommentInFilm_Film_FilmDbModelId",
                        column: x => x.FilmDbModelId,
                        principalTable: "Film",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentInSerial",
                columns: table => new
                {
                    FavoriteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialDbModelId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_CommentInSerial_Serial_SerialDbModelId",
                        column: x => x.SerialDbModelId,
                        principalTable: "Serial",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_CommentId",
                table: "CommentInFilm",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInFilm_FilmDbModelId",
                table: "CommentInFilm",
                column: "FilmDbModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_CommentId",
                table: "CommentInSerial",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInSerial_SerialDbModelId",
                table: "CommentInSerial",
                column: "SerialDbModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentInFilm");

            migrationBuilder.DropTable(
                name: "CommentInSerial");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FavoriteSerial_FavoriteId",
                table: "FavoriteSerial");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FavoriteFilm_FavoriteId",
                table: "FavoriteFilm");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "FavoriteSerial");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "FavoriteFilm");
        }
    }
}
