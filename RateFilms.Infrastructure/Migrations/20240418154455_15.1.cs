using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _151 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "ReviewDbModel");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "ReviewDbModel",
                newName: "IX_ReviewDbModel_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewDbModel",
                table: "ReviewDbModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewDbModel_User_UserId",
                table: "ReviewDbModel",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewDbModel_User_UserId",
                table: "ReviewDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewDbModel",
                table: "ReviewDbModel");

            migrationBuilder.RenameTable(
                name: "ReviewDbModel",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewDbModel_UserId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
