using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSeenToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "PageSettings");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "PageSettings",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }
    }
}
