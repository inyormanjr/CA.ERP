using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class changeStockMovePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMoves",
                table: "StockMoves");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StockMoves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StockMoves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "StockMoves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StockMoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StockMoves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "StockMoves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMoves",
                table: "StockMoves",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StockMoves_BranchId_MasterProductId",
                table: "StockMoves",
                columns: new[] { "BranchId", "MasterProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMoves",
                table: "StockMoves");

            migrationBuilder.DropIndex(
                name: "IX_StockMoves_BranchId_MasterProductId",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StockMoves");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "StockMoves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMoves",
                table: "StockMoves",
                columns: new[] { "BranchId", "MasterProductId" });
        }
    }
}
