using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAssetsTypeInUserAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetsType",
                table: "UserAssets");

            migrationBuilder.AlterColumn<decimal>(
                name: "AssetsGold",
                table: "UserAssets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssetsGold",
                table: "UserAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<byte>(
                name: "AssetsType",
                table: "UserAssets",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
