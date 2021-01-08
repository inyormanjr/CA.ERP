using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class serialRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockNumber",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "StockNumber",
                table: "Stocks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Stocks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockNumber",
                table: "Stocks",
                column: "StockNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockNumber",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "StockNumber",
                table: "Stocks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Stocks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
        }
    }
}
