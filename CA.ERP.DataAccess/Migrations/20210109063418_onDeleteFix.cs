using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class onDeleteFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterProducts_Brands_BrandId",
                table: "MasterProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_MasterProducts_MasterProductId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_ApprovedById",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_Branches_BranchId",
                table: "StockReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                table: "StockReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_MasterProducts_MasterProductId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Stocks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BranchId",
                table: "Stocks",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterProducts_Brands_BrandId",
                table: "MasterProducts",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_MasterProducts_MasterProductId",
                table: "PurchaseOrderItems",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchId",
                table: "PurchaseOrders",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_ApprovedById",
                table: "PurchaseOrders",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_Branches_BranchId",
                table: "StockReceives",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                table: "StockReceives",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Branches_BranchId",
                table: "Stocks",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_MasterProducts_MasterProductId",
                table: "Stocks",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks",
                column: "PurchaseOrderItemId",
                principalTable: "PurchaseOrderItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks",
                column: "StockReceiveId",
                principalTable: "StockReceives",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterProducts_Brands_BrandId",
                table: "MasterProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_MasterProducts_MasterProductId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_ApprovedById",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_Branches_BranchId",
                table: "StockReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                table: "StockReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Branches_BranchId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_MasterProducts_MasterProductId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_BranchId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterProducts_Brands_BrandId",
                table: "MasterProducts",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_MasterProducts_MasterProductId",
                table: "PurchaseOrderItems",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchId",
                table: "PurchaseOrders",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_ApprovedById",
                table: "PurchaseOrders",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_Branches_BranchId",
                table: "StockReceives",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                table: "StockReceives",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_MasterProducts_MasterProductId",
                table: "Stocks",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks",
                column: "PurchaseOrderItemId",
                principalTable: "PurchaseOrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks",
                column: "StockReceiveId",
                principalTable: "StockReceives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
