using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(nullable: true),
                    WarehouseCode = table.Column<string>(nullable: true),
                    OnWay = table.Column<int>(nullable: false),
                    Pending = table.Column<int>(nullable: false),
                    Sellable = table.Column<int>(nullable: false),
                    Unsellable = table.Column<int>(nullable: false),
                    Reserved = table.Column<int>(nullable: false),
                    Shipped = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
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
                name: "OperationLogs");

            migrationBuilder.DropTable(
                name: "PicturePaths");

            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
