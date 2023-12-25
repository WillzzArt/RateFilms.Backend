using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _130 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comment_CommentsId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_User_UsersId",
                table: "CommentLikes");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "CommentLikes",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "CommentsId",
                table: "CommentLikes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_UsersId",
                table: "CommentLikes",
                newName: "IX_CommentLikes_CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Comment_CommentId",
                table: "CommentLikes",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_User_UserId",
                table: "CommentLikes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comment_CommentId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_User_UserId",
                table: "CommentLikes");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "CommentLikes",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CommentLikes",
                newName: "CommentsId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLikes",
                newName: "IX_CommentLikes_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Comment_CommentsId",
                table: "CommentLikes",
                column: "CommentsId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_User_UsersId",
                table: "CommentLikes",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
