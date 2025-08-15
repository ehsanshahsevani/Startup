using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentParentIdToDocumentDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentDocumentId",
                table: "DocumentDetails",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetails_ParentDocumentId",
                table: "DocumentDetails",
                column: "ParentDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDetails_Documents_ParentDocumentId",
                table: "DocumentDetails",
                column: "ParentDocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDetails_Documents_ParentDocumentId",
                table: "DocumentDetails");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDetails_ParentDocumentId",
                table: "DocumentDetails");

            migrationBuilder.DropColumn(
                name: "ParentDocumentId",
                table: "DocumentDetails");
        }
    }
}
