using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigRelationInAccountCodingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountCodings_AccountCodings_AccountCodingId",
                table: "AccountCodings");

            migrationBuilder.DropIndex(
                name: "IX_AccountCodings_AccountCodingId",
                table: "AccountCodings");

            migrationBuilder.DropColumn(
                name: "AccountCodingId",
                table: "AccountCodings");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "AccountCodings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCodings_ParentId",
                table: "AccountCodings",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountCodings_AccountCodings_ParentId",
                table: "AccountCodings",
                column: "ParentId",
                principalTable: "AccountCodings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountCodings_AccountCodings_ParentId",
                table: "AccountCodings");

            migrationBuilder.DropIndex(
                name: "IX_AccountCodings_ParentId",
                table: "AccountCodings");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "AccountCodings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "AccountCodingId",
                table: "AccountCodings",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCodings_AccountCodingId",
                table: "AccountCodings",
                column: "AccountCodingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountCodings_AccountCodings_AccountCodingId",
                table: "AccountCodings",
                column: "AccountCodingId",
                principalTable: "AccountCodings",
                principalColumn: "Id");
        }
    }
}
