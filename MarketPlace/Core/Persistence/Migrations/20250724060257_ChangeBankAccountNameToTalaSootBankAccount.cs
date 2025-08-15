using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBankAccountNameToTalaSootBankAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBanks_BankAccounts_BankAccountId",
                table: "ProfileBanks");

            migrationBuilder.DropIndex(
                name: "IX_ProfileBanks_BankAccountId",
                table: "ProfileBanks");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "ProfileBanks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountId",
                table: "ProfileBanks",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBanks_BankAccountId",
                table: "ProfileBanks",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBanks_BankAccounts_BankAccountId",
                table: "ProfileBanks",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }
    }
}
