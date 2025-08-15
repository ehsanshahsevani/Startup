using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePageSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageSettings_Categories_CategoryId",
                table: "PageSettings");

            migrationBuilder.DropIndex(
                name: "IX_PageSettings_CategoryId",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "HeightImageInWeb",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "IsActiveInMobile",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "IsActiveInWeb",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "WidthImageInWeb",
                table: "PageSettings");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "PageSettings",
                newName: "Question");

            migrationBuilder.AddColumn<bool>(
                name: "OnDelete",
                table: "TagPageSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "PageSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNameIcon",
                table: "PageSettings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileOriginalNameIcon",
                table: "PageSettings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupByName",
                table: "PageSettings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnDelete",
                table: "TagPageSettings");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "FileNameIcon",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "FileOriginalNameIcon",
                table: "PageSettings");

            migrationBuilder.DropColumn(
                name: "GroupByName",
                table: "PageSettings");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "PageSettings",
                newName: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "PageSettings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightImageInWeb",
                table: "PageSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveInMobile",
                table: "PageSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveInWeb",
                table: "PageSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WidthImageInWeb",
                table: "PageSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageSettings_CategoryId",
                table: "PageSettings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageSettings_Categories_CategoryId",
                table: "PageSettings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
