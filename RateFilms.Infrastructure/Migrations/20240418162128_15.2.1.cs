using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1521 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Comment",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Comment",
                newName: "status");
        }
    }
}
