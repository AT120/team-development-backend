using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamDevelopmentBackend.Migrations
{
    /// <inheritdoc />
    public partial class Profilactic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Subjects",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subjects",
                newName: "name");
        }
    }
}
