using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class addStockInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockInventories",
                columns: table => new
                {
                    MasterProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInventories", x => new { x.BranchId, x.MasterProductId });
                    table.ForeignKey(
                        name: "FK_StockInventories_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInventories_MasterProducts_MasterProductId",
                        column: x => x.MasterProductId,
                        principalTable: "MasterProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMoves",
                columns: table => new
                {
                    MasterProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoveCause = table.Column<int>(type: "int", nullable: false),
                    PreviousQuantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ChangeQuantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    CurrentQuantity = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    StockReceiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMoves", x => new { x.BranchId, x.MasterProductId });
                    table.ForeignKey(
                        name: "FK_StockMoves_StockInventories_BranchId_MasterProductId",
                        columns: x => new { x.BranchId, x.MasterProductId },
                        principalTable: "StockInventories",
                        principalColumns: new[] { "BranchId", "MasterProductId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMoves_StockReceives_StockReceiveId",
                        column: x => x.StockReceiveId,
                        principalTable: "StockReceives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockInventories_MasterProductId",
                table: "StockInventories",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMoves_StockReceiveId",
                table: "StockMoves",
                column: "StockReceiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMoves");

            migrationBuilder.DropTable(
                name: "StockInventories");
        }
    }
}
