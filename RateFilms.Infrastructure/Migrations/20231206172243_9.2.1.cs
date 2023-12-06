using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _921 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDbModelUserDbModel_Comment_CommentsId",
                table: "CommentDbModelUserDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDbModelUserDbModel_User_UsersId",
                table: "CommentDbModelUserDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentDbModelUserDbModel",
                table: "CommentDbModelUserDbModel");

            migrationBuilder.RenameTable(
                name: "CommentDbModelUserDbModel",
                newName: "CommentLikes");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDbModelUserDbModel_UsersId",
                table: "CommentLikes",
                newName: "IX_CommentLikes_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes",
                columns: new[] { "CommentsId", "UsersId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comment_CommentsId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_User_UsersId",
                table: "CommentLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes");

            migrationBuilder.RenameTable(
                name: "CommentLikes",
                newName: "CommentDbModelUserDbModel");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_UsersId",
                table: "CommentDbModelUserDbModel",
                newName: "IX_CommentDbModelUserDbModel_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentDbModelUserDbModel",
                table: "CommentDbModelUserDbModel",
                columns: new[] { "CommentsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDbModelUserDbModel_Comment_CommentsId",
                table: "CommentDbModelUserDbModel",
                column: "CommentsId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDbModelUserDbModel_User_UsersId",
                table: "CommentDbModelUserDbModel",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
