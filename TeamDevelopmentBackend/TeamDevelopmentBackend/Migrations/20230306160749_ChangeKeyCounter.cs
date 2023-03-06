using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamDevelopmentBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Counter",
                table: "Counter");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Counter",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Counter",
                table: "Counter",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Counter",
                table: "Counter");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Counter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Counter",
                table: "Counter",
                column: "Last");
        }
    }
}
