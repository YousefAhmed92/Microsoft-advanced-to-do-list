using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks01.Migrations
{
    /// <inheritdoc />
    public partial class nullableLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Lists_ListsId",
                table: "Task");

            migrationBuilder.AlterColumn<int>(
                name: "ListsId",
                table: "Task",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Lists_ListsId",
                table: "Task",
                column: "ListsId",
                principalTable: "Lists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Lists_ListsId",
                table: "Task");

            migrationBuilder.AlterColumn<int>(
                name: "ListsId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Lists_ListsId",
                table: "Task",
                column: "ListsId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
