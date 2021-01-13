using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class changeSupplierBrandPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands");

            migrationBuilder.DropIndex(
                name: "IX_SupplierBrands_SupplierId",
                table: "SupplierBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands",
                columns: new[] { "SupplierId", "BrandId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierBrands",
                table: "SupplierBrands",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBrands_SupplierId",
                table: "SupplierBrands",
                column: "SupplierId");
        }
    }
}
