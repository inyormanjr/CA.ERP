using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class RemoveUniqueOnStockReceiveItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockReceiveItems_SerialNumber",
                table: "StockReceiveItems");

            migrationBuilder.DropIndex(
                name: "IX_StockReceiveItems_StockNumber",
                table: "StockReceiveItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_SerialNumber",
                table: "StockReceiveItems",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_StockNumber",
                table: "StockReceiveItems",
                column: "StockNumber",
                unique: true);
        }
    }
}
