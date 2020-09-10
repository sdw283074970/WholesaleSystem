using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class ResumeAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
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
                    Warehouse_desc = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    TypeLayer = table.Column<int>(nullable: false),
                    TypeCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PicturePaths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    UploadBy = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    IsMainPicture = table.Column<bool>(nullable: false),
                    InventoryId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "InventoryProductTypes",
                columns: table => new
                {
                    InventoryId = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
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
                name: "OperationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(nullable: true),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    OperatedBy = table.Column<string>(nullable: true),
                    InventoryId = table.Column<int>(nullable: true),
                    PicturePathId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationLogs_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperationLogs_PicturePaths_PicturePathId",
                        column: x => x.PicturePathId,
                        principalTable: "PicturePaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProductTypes_ProductTypeId",
                table: "InventoryProductTypes",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_InventoryId",
                table: "OperationLogs",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_PicturePathId",
                table: "OperationLogs",
                column: "PicturePathId");

            migrationBuilder.CreateIndex(
                name: "IX_PicturePaths_InventoryId",
                table: "PicturePaths",
                column: "InventoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryProductTypes");

            migrationBuilder.DropTable(
                name: "OperationLogs");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "PicturePaths");

            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
