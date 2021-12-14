using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Migrations
{
    public partial class ColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "A",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "B",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "G",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "R",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "B",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "G",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "R",
                table: "Categories");
        }
    }
}
