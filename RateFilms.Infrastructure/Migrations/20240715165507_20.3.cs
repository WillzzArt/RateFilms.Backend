using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _203 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteMovieId_FavoriteUserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_FavoriteMovieId_FavoriteUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "FavoriteMovieId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "FavoriteUserId",
                table: "Comment");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FavoriteMovie_Id",
                table: "FavoriteMovie",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FavoriteId",
                table: "Comment",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteId",
                table: "Comment",
                column: "FavoriteId",
                principalTable: "FavoriteMovie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteId",
                table: "Comment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FavoriteMovie_Id",
                table: "FavoriteMovie");

            migrationBuilder.DropIndex(
                name: "IX_Comment_FavoriteId",
                table: "Comment");

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

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FavoriteMovieId_FavoriteUserId",
                table: "Comment",
                columns: new[] { "FavoriteMovieId", "FavoriteUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_FavoriteMovie_FavoriteMovieId_FavoriteUserId",
                table: "Comment",
                columns: new[] { "FavoriteMovieId", "FavoriteUserId" },
                principalTable: "FavoriteMovie",
                principalColumns: new[] { "MovieId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
