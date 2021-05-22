using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class AddStockIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Stocks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Stocks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CreatedAt",
                table: "Stocks",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockStatus",
                table: "Stocks",
                column: "StockStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UpdatedAt",
                table: "Stocks",
                column: "UpdatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_CreatedAt",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockStatus",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_UpdatedAt",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Stocks");
        }
    }
}
