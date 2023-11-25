using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "Serial");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Serial",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Serial",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreviewImageId",
                table: "Serial",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvgRating = table.Column<float>(type: "real", nullable: true),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Series_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Serial_PreviewImageId",
                table: "Serial",
                column: "PreviewImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ImageId",
                table: "Series",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_SeasonId",
                table: "Series",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Serial_Image_PreviewImageId",
                table: "Serial",
                column: "PreviewImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Serial_Image_PreviewImageId",
                table: "Serial");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Serial_PreviewImageId",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "PreviewImageId",
                table: "Serial");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Serial",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "Serial",
                type: "text",
                nullable: true);
        }
    }
}
