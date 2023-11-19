using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskPlanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateDateTypeForReminders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            Down(migrationBuilder);
            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReminderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReminderRepeat = table.Column<bool>(type: "bit", nullable: false),
                    ReminderRepeatType = table.Column<int>(type: "int", nullable: false),
                    ReminderRepeatInterval = table.Column<int>(type: "int", nullable: false),
                    IsSyncReminderWithCalendar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.ReminderId);
                    table.ForeignKey(
                        name: "FK_Reminders_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_TaskId",
                table: "Reminders",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Reminders");
        }
    }
}
