using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class changeSupplierMasterProductPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierMasterProducts",
                table: "SupplierMasterProducts");

            migrationBuilder.DropIndex(
                name: "IX_SupplierMasterProducts_SupplierId",
                table: "SupplierMasterProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SupplierMasterProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SupplierBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierMasterProducts",
                table: "SupplierMasterProducts",
                columns: new[] { "SupplierId", "MasterProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierMasterProducts",
                table: "SupplierMasterProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SupplierMasterProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SupplierBrands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierMasterProducts",
                table: "SupplierMasterProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierMasterProducts_SupplierId",
                table: "SupplierMasterProducts",
                column: "SupplierId");
        }
    }
}
