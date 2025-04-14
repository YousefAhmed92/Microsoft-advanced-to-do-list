using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks01.Migrations
{
    /// <inheritdoc />
    public partial class addplannedtaskstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlannedTaskId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlannedTasks",
                columns: table => new
                {
                    PlannedTasksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannedCategories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedTasks", x => x.PlannedTasksId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_PlannedTaskId",
                table: "Task",
                column: "PlannedTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_PlannedTasks_PlannedTaskId",
                table: "Task",
                column: "PlannedTaskId",
                principalTable: "PlannedTasks",
                principalColumn: "PlannedTasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_PlannedTasks_PlannedTaskId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "PlannedTasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_PlannedTaskId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "PlannedTaskId",
                table: "Task");
        }
    }
}
