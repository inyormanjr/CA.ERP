using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class addSupplierIdOnStockReceive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "StockReceives",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_SupplierId",
                table: "StockReceives",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives");

            migrationBuilder.DropIndex(
                name: "IX_StockReceives_SupplierId",
                table: "StockReceives");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "StockReceives");
        }
    }
}
