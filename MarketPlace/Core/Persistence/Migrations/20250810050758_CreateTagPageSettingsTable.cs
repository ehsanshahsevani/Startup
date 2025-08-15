using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTagPageSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagPageSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPageSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageSettingTagPageSetting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PageSettingId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TagPageSettingId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSettingTagPageSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSettingTagPageSetting_PageSettings_PageSettingId",
                        column: x => x.PageSettingId,
                        principalTable: "PageSettings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PageSettingTagPageSetting_TagPageSettings_TagPageSettingId",
                        column: x => x.TagPageSettingId,
                        principalTable: "TagPageSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageSettingTagPageSetting_PageSettingId",
                table: "PageSettingTagPageSetting",
                column: "PageSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSettingTagPageSetting_TagPageSettingId",
                table: "PageSettingTagPageSetting",
                column: "TagPageSettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageSettingTagPageSetting");

            migrationBuilder.DropTable(
                name: "TagPageSettings");
        }
    }
}
