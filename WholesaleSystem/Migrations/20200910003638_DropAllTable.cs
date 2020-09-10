using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class DropAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationLogs");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "PicturePaths");

            migrationBuilder.DropTable(
                name: "Inventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Onway = table.Column<int>(type: "int", nullable: false),
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
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeLayer = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
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
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "OperationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    OperatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PicturePathId = table.Column<int>(type: "int", nullable: true)
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
    }
}
