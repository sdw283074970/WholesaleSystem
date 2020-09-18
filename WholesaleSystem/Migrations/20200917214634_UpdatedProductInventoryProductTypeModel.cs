using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class UpdatedProductInventoryProductTypeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_InventoryId",
                table: "ImageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryProductTypes_ProdectuInventories_InventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventoryProductTypes",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_InventoryId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "ImageFiles");

            migrationBuilder.AddColumn<int>(
                name: "ProductInventoryId",
                table: "ProductInventoryProductTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductInventoryId",
                table: "ImageFiles",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventoryProductTypes",
                table: "ProductInventoryProductTypes",
                columns: new[] { "ProductInventoryId", "ProductTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_ProductInventoryId",
                table: "ImageFiles",
                column: "ProductInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_ProductInventoryId",
                table: "ImageFiles",
                column: "ProductInventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryProductTypes_ProdectuInventories_ProductInventoryId",
                table: "ProductInventoryProductTypes",
                column: "ProductInventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_ProductInventoryId",
                table: "ImageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryProductTypes_ProdectuInventories_ProductInventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventoryProductTypes",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_ProductInventoryId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "ProductInventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductInventoryId",
                table: "ImageFiles");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "ProductInventoryProductTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "ImageFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventoryProductTypes",
                table: "ProductInventoryProductTypes",
                columns: new[] { "InventoryId", "ProductTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_InventoryId",
                table: "ImageFiles",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_InventoryId",
                table: "ImageFiles",
                column: "InventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryProductTypes_ProdectuInventories_InventoryId",
                table: "ProductInventoryProductTypes",
                column: "InventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
