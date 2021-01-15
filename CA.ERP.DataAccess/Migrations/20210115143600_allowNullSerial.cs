using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class allowNullSerial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Stocks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "StockCounters",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCounters", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks",
                column: "SerialNumber",
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockCounters");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SerialNumber",
                table: "Stocks");

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
        }
    }
}
