using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class AddSupplierToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks");

            migrationBuilder.AlterColumn<Guid>(
                name: "StockReceiveId",
                table: "Stocks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Stocks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StockId",
                table: "StockReceiveItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Employer = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EmployerAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CoMaker = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CoMakerAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SupplierId",
                table: "Stocks",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveItems_StockId",
                table: "StockReceiveItems",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FirstName",
                table: "Customers",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastName",
                table: "Customers",
                column: "LastName");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceiveItems_Stocks_StockId",
                table: "StockReceiveItems",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Suppliers_SupplierId",
                table: "Stocks",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceiveItems_Stocks_StockId",
                table: "StockReceiveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_PurchaseOrderItems_PurchaseOrderItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockReceives_StockReceiveId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Suppliers_SupplierId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SupplierId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_StockReceiveItems_StockId",
                table: "StockReceiveItems");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StockReceiveItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "StockReceiveId",
                table: "Stocks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
    }
}
