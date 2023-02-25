using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamDevelopmentBackend.Migrations
{
    /// <inheritdoc />
    public partial class EndDateDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Lessons",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(9999, 12, 31),
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Lessons",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(9999, 12, 31));
        }
    }
}
