using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class AddStockCounterAndStockReceive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockCounters",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Counter = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCounters", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "StockReceives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockSouce = table.Column<int>(type: "integer", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeliveryReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceives_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockReceives_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockReceives_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockReceiveItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MasterProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockReceiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseOrderItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockNumber = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StockStatus = table.Column<int>(type: "integer", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiveItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceiveItems_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockReceiveItems_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockReceiveItems_PurchaseOrderItems_PurchaseOrderItemId",
                        column: x => x.PurchaseOrderItemId,
                        principalTable: "PurchaseOrderItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockReceiveItems_StockReceives_StockReceiveId",
                        column: x => x.StockReceiveId,
                        principalTable: "StockReceives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_BranchId",
                table: "StockReceiveItems",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_MasterProductId",
                table: "StockReceiveItems",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_PurchaseOrderItemId",
                table: "StockReceiveItems",
                column: "PurchaseOrderItemId");

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

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_StockReceiveId",
                table: "StockReceiveItems",
                column: "StockReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_BranchId",
                table: "StockReceives",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_PurchaseOrderId",
                table: "StockReceives",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceives_SupplierId",
                table: "StockReceives",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockCounters");

            migrationBuilder.DropTable(
                name: "StockReceiveItems");

            migrationBuilder.DropTable(
                name: "StockReceives");
        }
    }
}
