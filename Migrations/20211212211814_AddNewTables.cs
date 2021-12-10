using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyChanges_Accounts_AccountID",
                table: "MoneyChanges");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "MoneyChanges",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_MoneyChanges_AccountID",
                table: "MoneyChanges",
                newName: "IX_MoneyChanges_AccountId");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Accounts",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "MoneyChanges",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyChanges_Accounts_AccountId",
                table: "MoneyChanges",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyChanges_Accounts_AccountId",
                table: "MoneyChanges");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "MoneyChanges",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_MoneyChanges_AccountId",
                table: "MoneyChanges",
                newName: "IX_MoneyChanges_AccountID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountID");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "MoneyChanges",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyChanges_Accounts_AccountID",
                table: "MoneyChanges",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
