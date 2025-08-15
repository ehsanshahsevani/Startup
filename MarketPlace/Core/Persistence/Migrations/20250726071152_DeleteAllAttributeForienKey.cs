using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAllAttributeForienKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBanks_Banks_ProfileId",
                table: "ProfileBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBanks_Profiles_ProfileId",
                table: "ProfileBanks");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBanks_BankId",
                table: "ProfileBanks",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBanks_Banks_BankId",
                table: "ProfileBanks",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBanks_Profiles_ProfileId",
                table: "ProfileBanks",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBanks_Banks_BankId",
                table: "ProfileBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBanks_Profiles_ProfileId",
                table: "ProfileBanks");

            migrationBuilder.DropIndex(
                name: "IX_ProfileBanks_BankId",
                table: "ProfileBanks");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBanks_Banks_ProfileId",
                table: "ProfileBanks",
                column: "ProfileId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBanks_Profiles_ProfileId",
                table: "ProfileBanks",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
