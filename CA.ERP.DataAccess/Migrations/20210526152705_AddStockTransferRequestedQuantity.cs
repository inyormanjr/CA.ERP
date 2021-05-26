using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class AddStockTransferRequestedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedQuantity",
                table: "StockTransferItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedQuantity",
                table: "StockTransferItems");
        }
    }
}
