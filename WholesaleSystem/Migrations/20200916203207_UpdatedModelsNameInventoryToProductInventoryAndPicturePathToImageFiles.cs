using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class UpdatedModelsNameInventoryToProductInventoryAndPicturePathToImageFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_Inventories_InventoryId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_PicturePaths_PicturePathId",
                table: "OperationLogs");

            migrationBuilder.DropTable(
                name: "InventoryProductTypes");

            migrationBuilder.DropTable(
                name: "PicturePaths");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_InventoryId",
                table: "OperationLogs");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_PicturePathId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "PicturePathId",
                table: "OperationLogs");

            migrationBuilder.AddColumn<int>(
                name: "ImageFileId",
                table: "OperationLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductInventoryId",
                table: "OperationLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProdectuInventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_barcode = table.Column<string>(nullable: true),
                    Product_sku = table.Column<string>(nullable: true),
                    Reference_no = table.Column<string>(nullable: true),
                    Product_title = table.Column<string>(nullable: true),
                    Product_title_en = table.Column<string>(nullable: true),
                    Warehouse_code = table.Column<string>(nullable: true),
                    Onway = table.Column<int>(nullable: false),
                    Pending = table.Column<int>(nullable: false),
                    Sellable = table.Column<int>(nullable: false),
                    Unsellable = table.Column<int>(nullable: false),
                    Reserved = table.Column<int>(nullable: false),
                    Shipped = table.Column<int>(nullable: false),
                    Sold_share = table.Column<int>(nullable: false),
                    Pi_update_time = table.Column<DateTime>(nullable: false),
                    Shared = table.Column<int>(nullable: false),
                    OriginalPrice = table.Column<float>(nullable: false),
                    Warehouse_desc = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdectuInventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    UploadBy = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    IsMainPicture = table.Column<bool>(nullable: false),
                    InventoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageFiles_ProdectuInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "ProdectuInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryProductTypes",
                columns: table => new
                {
                    InventoryId = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryProductTypes", x => new { x.InventoryId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_ProductInventoryProductTypes_ProdectuInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "ProdectuInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventoryProductTypes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_ImageFileId",
                table: "OperationLogs",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_ProductInventoryId",
                table: "OperationLogs",
                column: "ProductInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_InventoryId",
                table: "ImageFiles",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryProductTypes_ProductTypeId",
                table: "ProductInventoryProductTypes",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_ImageFiles_ImageFileId",
                table: "OperationLogs",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_ProdectuInventories_ProductInventoryId",
                table: "OperationLogs",
                column: "ProductInventoryId",
                principalTable: "ProdectuInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_ImageFiles_ImageFileId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_ProdectuInventories_ProductInventoryId",
                table: "OperationLogs");

            migrationBuilder.DropTable(
                name: "ImageFiles");

            migrationBuilder.DropTable(
                name: "ProductInventoryProductTypes");

            migrationBuilder.DropTable(
                name: "ProdectuInventories");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_ImageFileId",
                table: "OperationLogs");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_ProductInventoryId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "ImageFileId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "ProductInventoryId",
                table: "OperationLogs");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "OperationLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PicturePathId",
                table: "OperationLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Onway = table.Column<int>(type: "int", nullable: false),
                    OriginalPrice = table.Column<float>(type: "real", nullable: false),
                    Pending = table.Column<int>(type: "int", nullable: false),
                    Pi_update_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product_barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_title_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reserved = table.Column<int>(type: "int", nullable: false),
                    Sellable = table.Column<int>(type: "int", nullable: false),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    Shipped = table.Column<int>(type: "int", nullable: false),
                    Sold_share = table.Column<int>(type: "int", nullable: false),
                    Unsellable = table.Column<int>(type: "int", nullable: false),
                    Warehouse_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warehouse_desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryProductTypes",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProductTypes", x => new { x.InventoryId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_InventoryProductTypes_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryProductTypes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PicturePaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    IsMainPicture = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicturePaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicturePaths_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_InventoryId",
                table: "OperationLogs",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_PicturePathId",
                table: "OperationLogs",
                column: "PicturePathId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProductTypes_ProductTypeId",
                table: "InventoryProductTypes",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PicturePaths_InventoryId",
                table: "PicturePaths",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Inventories_InventoryId",
                table: "OperationLogs",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_PicturePaths_PicturePathId",
                table: "OperationLogs",
                column: "PicturePathId",
                principalTable: "PicturePaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
