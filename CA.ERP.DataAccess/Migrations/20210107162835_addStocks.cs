using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class addStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockReceives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSouce = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceives_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockReceiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StockStatus = table.Column<int>(type: "int", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                        column: x => x.PurchaseOrderItemId,
                        principalTable: "PurchaseOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stocks_StockReceives_StockReceiveId",
                        column: x => x.StockReceiveId,
                        principalTable: "StockReceives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_BranchId",
                table: "StockReceives",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_PurchaseOrderId",
                table: "StockReceives",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MasterProductId",
                table: "Stocks",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PurchaseOrderItemId",
                table: "Stocks",
                column: "PurchaseOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks",
                column: "SerialNumber",
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockNumber",
                table: "Stocks",
                column: "StockNumber",
                unique: true,
                filter: "[StockNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockReceiveId",
                table: "Stocks",
                column: "StockReceiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StockReceives");
        }
    }
}
