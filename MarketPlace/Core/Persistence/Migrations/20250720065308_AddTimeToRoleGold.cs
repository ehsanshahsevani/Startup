using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeToRoleGold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Min",
                table: "RoleGolds");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "RoleGolds",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "RoleGolds",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "RoleGolds");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "RoleGolds");

            migrationBuilder.AddColumn<int>(
                name: "Min",
                table: "RoleGolds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
