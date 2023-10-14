using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Serials_SerialId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_SerialId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "SerialId",
                table: "Series");

            migrationBuilder.RenameColumn(
                name: "avgRating",
                table: "Films",
                newName: "AvgRating");

            migrationBuilder.RenameColumn(
                name: "ageRating",
                table: "Films",
                newName: "AgeRating");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
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

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RealeseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AvgRating = table.Column<float>(type: "real", nullable: false),
                    SerialId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Serials_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    FilmId = table.Column<Guid>(type: "uuid", nullable: true),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    FilmId = table.Column<Guid>(type: "uuid", nullable: true),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actors_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actors_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_FilmId",
                table: "Actors",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ImageId",
                table: "Actors",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FilmId",
                table: "Images",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SeasonId",
                table: "Images",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SerialId",
                table: "Seasons",
                column: "SerialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "AvgRating",
                table: "Films",
                newName: "avgRating");

            migrationBuilder.RenameColumn(
                name: "AgeRating",
                table: "Films",
                newName: "ageRating");

            migrationBuilder.AddColumn<Guid>(
                name: "SerialId",
                table: "Series",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Films",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Films",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Series_SerialId",
                table: "Series",
                column: "SerialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Serials_SerialId",
                table: "Series",
                column: "SerialId",
                principalTable: "Serials",
                principalColumn: "Id");
        }
    }
}
