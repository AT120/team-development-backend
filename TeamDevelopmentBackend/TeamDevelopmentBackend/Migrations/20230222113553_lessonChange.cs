using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamDevelopmentBackend.Migrations
{
    /// <inheritdoc />
    public partial class lessonChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_TimeSlot_Date_GroupId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TimeSlot_Date_RoomId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TimeSlot_Date_TeacherId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Lessons",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Lessons",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Lessons",
                newName: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimeSlot_Date_GroupId",
                table: "Lessons",
                columns: new[] { "TimeSlot", "Date", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimeSlot_Date_RoomId",
                table: "Lessons",
                columns: new[] { "TimeSlot", "Date", "RoomId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimeSlot_Date_TeacherId",
                table: "Lessons",
                columns: new[] { "TimeSlot", "Date", "TeacherId" },
                unique: true);
        }
    }
}
