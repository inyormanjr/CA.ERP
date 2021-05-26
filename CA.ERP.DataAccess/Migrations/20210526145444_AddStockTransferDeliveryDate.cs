using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class AddStockTransferDeliveryDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_Stocks_StockId",
                table: "StockTransferItems");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "StockTransferItems",
                newName: "MasterProductId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItems_StockId",
                table: "StockTransferItems",
                newName: "IX_StockTransferItems_MasterProductId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeliveryDate",
                table: "StockTransfers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_MasterProducts_MasterProductId",
                table: "StockTransferItems",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_MasterProducts_MasterProductId",
                table: "StockTransferItems");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "StockTransfers");

            migrationBuilder.RenameColumn(
                name: "MasterProductId",
                table: "StockTransferItems",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItems_MasterProductId",
                table: "StockTransferItems",
                newName: "IX_StockTransferItems_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_Stocks_StockId",
                table: "StockTransferItems",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
