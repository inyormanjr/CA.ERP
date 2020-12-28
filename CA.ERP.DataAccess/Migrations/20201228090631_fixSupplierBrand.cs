using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class fixSupplierBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierBrands_Brands_SupplierId",
                table: "SupplierBrands");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBrands_BrandId",
                table: "SupplierBrands",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierBrands_Brands_BrandId",
                table: "SupplierBrands",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierBrands_Brands_BrandId",
                table: "SupplierBrands");

            migrationBuilder.DropIndex(
                name: "IX_SupplierBrands_BrandId",
                table: "SupplierBrands");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierBrands_Brands_SupplierId",
                table: "SupplierBrands",
                column: "SupplierId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
