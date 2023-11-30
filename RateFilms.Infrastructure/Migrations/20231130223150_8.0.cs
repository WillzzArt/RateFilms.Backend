using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _80 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionId",
                table: "PersonInSerialProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Image_ImageId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_ImageId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "SeriesCount",
                table: "Serial");

            migrationBuilder.RenameColumn(
                name: "ProfessionId",
                table: "PersonInSerialProfession",
                newName: "ProfessionsId");

            migrationBuilder.AddColumn<Guid>(
                name: "PreviewImageId",
                table: "Series",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RealeseDate",
                table: "Series",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "RealeseDate",
                table: "Serial",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Series_PreviewImageId",
                table: "Series",
                column: "PreviewImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionsId",
                table: "PersonInSerialProfession",
                column: "ProfessionsId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Image_PreviewImageId",
                table: "Series",
                column: "PreviewImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionsId",
                table: "PersonInSerialProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Image_PreviewImageId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_PreviewImageId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "PreviewImageId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "RealeseDate",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "RealeseDate",
                table: "Serial");

            migrationBuilder.RenameColumn(
                name: "ProfessionsId",
                table: "PersonInSerialProfession",
                newName: "ProfessionId");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Serial",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeriesCount",
                table: "Serial",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_ImageId",
                table: "Series",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionId",
                table: "PersonInSerialProfession",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Image_ImageId",
                table: "Series",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }
    }
}
