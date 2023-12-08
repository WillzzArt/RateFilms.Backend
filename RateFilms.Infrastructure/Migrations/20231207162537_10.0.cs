using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Serial",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SerialId",
                table: "Season",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Film",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "FavoriteSerial",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Serial");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Film");

            migrationBuilder.AlterColumn<Guid>(
                name: "SerialId",
                table: "Season",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "FavoriteSerial",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Serial_SerialId",
                table: "Season",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id");
        }
    }
}
