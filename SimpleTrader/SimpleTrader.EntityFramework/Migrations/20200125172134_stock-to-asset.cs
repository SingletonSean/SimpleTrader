using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTrader.EntityFramework.Migrations
{
    public partial class stocktoasset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock_Symbol",
                table: "AssetTransactions",
                newName: "Asset_Symbol");

            migrationBuilder.RenameColumn(
                name: "Stock_PricePerShare",
                table: "AssetTransactions",
                newName: "Asset_PricePerShare");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Asset_Symbol",
                table: "AssetTransactions",
                newName: "Stock_Symbol");

            migrationBuilder.RenameColumn(
                name: "Asset_PricePerShare",
                table: "AssetTransactions",
                newName: "Stock_PricePerShare");
        }
    }
}
