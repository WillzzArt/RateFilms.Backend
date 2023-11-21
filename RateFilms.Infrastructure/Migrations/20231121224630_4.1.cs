using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Serials_SerialDbModelId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Serials_SerialDbModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SerialDbModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Actors_SerialDbModelId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "SerialDbModelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SerialDbModelId",
                table: "Actors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SerialDbModelId",
                table: "Images",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SerialDbModelId",
                table: "Actors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_SerialDbModelId",
                table: "Images",
                column: "SerialDbModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SerialDbModelId",
                table: "Actors",
                column: "SerialDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Serials_SerialDbModelId",
                table: "Actors",
                column: "SerialDbModelId",
                principalTable: "Serials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Serials_SerialDbModelId",
                table: "Images",
                column: "SerialDbModelId",
                principalTable: "Serials",
                principalColumn: "Id");
        }
    }
}
