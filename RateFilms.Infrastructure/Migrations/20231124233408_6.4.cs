using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_Proffesion_ProfessionId",
                table: "PersonInFilmProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_Proffesion_ProfessionId",
                table: "PersonInSerialProfession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proffesion",
                table: "Proffesion");

            migrationBuilder.RenameTable(
                name: "Proffesion",
                newName: "Profession");

            migrationBuilder.RenameIndex(
                name: "IX_Proffesion_Profession",
                table: "Profession",
                newName: "IX_Profession_Profession");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profession",
                table: "Profession",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionId",
                table: "PersonInFilmProfession",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionId",
                table: "PersonInSerialProfession",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionId",
                table: "PersonInFilmProfession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInSerialProfession_Profession_ProfessionId",
                table: "PersonInSerialProfession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profession",
                table: "Profession");

            migrationBuilder.RenameTable(
                name: "Profession",
                newName: "Proffesion");

            migrationBuilder.RenameIndex(
                name: "IX_Profession_Profession",
                table: "Proffesion",
                newName: "IX_Proffesion_Profession");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proffesion",
                table: "Proffesion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_Proffesion_ProfessionId",
                table: "PersonInFilmProfession",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInSerialProfession_Proffesion_ProfessionId",
                table: "PersonInSerialProfession",
                column: "ProfessionId",
                principalTable: "Proffesion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
