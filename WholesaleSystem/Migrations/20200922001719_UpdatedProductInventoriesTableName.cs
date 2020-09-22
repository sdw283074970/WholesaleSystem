using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class UpdatedProductInventoriesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_ProductInventoryId",
                table: "ImageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_ProdectuInventories_ProductInventoryId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryProductTypes_ProdectuInventories_ProductInventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdectuInventories",
                table: "ProdectuInventories");

            migrationBuilder.RenameTable(
                name: "ProdectuInventories",
                newName: "ProductInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_ProductInventories_ProductInventoryId",
                table: "ImageFiles",
                column: "ProductInventoryId",
                principalTable: "ProductInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_ProductInventories_ProductInventoryId",
                table: "OperationLogs",
                column: "ProductInventoryId",
                principalTable: "ProductInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryProductTypes_ProductInventories_ProductInventoryId",
                table: "ProductInventoryProductTypes",
                column: "ProductInventoryId",
                principalTable: "ProductInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_ProductInventories_ProductInventoryId",
                table: "ImageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_ProductInventories_ProductInventoryId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryProductTypes_ProductInventories_ProductInventoryId",
                table: "ProductInventoryProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories");

            migrationBuilder.RenameTable(
                name: "ProductInventories",
                newName: "ProdectuInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdectuInventories",
                table: "ProdectuInventories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_ProdectuInventories_ProductInventoryId",
                table: "ImageFiles",
                column: "ProductInventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_ProdectuInventories_ProductInventoryId",
                table: "OperationLogs",
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
    }
}
