using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class EditStockTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives");

            migrationBuilder.AddColumn<int>(
                name: "StockTransferStatus",
                table: "StockTransfers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "StockReceives",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "StockTransferId",
                table: "StockReceives",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_StockTransferId",
                table: "StockReceives",
                column: "StockTransferId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_StockTransfers_StockTransferId",
                table: "StockReceives",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_StockTransfers_StockTransferId",
                table: "StockReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives");

            migrationBuilder.DropIndex(
                name: "IX_StockReceives_StockTransferId",
                table: "StockReceives");

            migrationBuilder.DropColumn(
                name: "StockTransferStatus",
                table: "StockTransfers");

            migrationBuilder.DropColumn(
                name: "StockTransferId",
                table: "StockReceives");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "StockReceives",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_Suppliers_SupplierId",
                table: "StockReceives",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
