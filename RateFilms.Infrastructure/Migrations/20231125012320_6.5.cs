using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionId",
                table: "PersonInFilmProfession");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "Film");

            migrationBuilder.RenameColumn(
                name: "ProfessionId",
                table: "PersonInFilmProfession",
                newName: "ProfessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionsId",
                table: "PersonInFilmProfession",
                column: "ProfessionsId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionsId",
                table: "PersonInFilmProfession");

            migrationBuilder.RenameColumn(
                name: "ProfessionsId",
                table: "PersonInFilmProfession",
                newName: "ProfessionId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Film",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "Film",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInFilmProfession_Profession_ProfessionId",
                table: "PersonInFilmProfession",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
