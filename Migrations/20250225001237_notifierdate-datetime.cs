using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks01.Migrations
{
    /// <inheritdoc />
    public partial class notifierdatedatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NotifierWhen",
                table: "Task",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifierWhen",
                table: "Task");
        }
    }
}
