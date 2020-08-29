using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WholesaleSystem.Migrations
{
    public partial class UpdatedInventoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "OnWay",
                table: "Inventories",
                newName: "Onway");

            migrationBuilder.AddColumn<DateTime>(
                name: "Pi_update_time",
                table: "Inventories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Product_barcode",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product_sku",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product_title",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product_title_en",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference_no",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shared",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sold_share",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Warehouse_code",
                table: "Inventories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Warehouse_desc",
                table: "Inventories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pi_update_time",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Product_barcode",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Product_sku",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Product_title",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Product_title_en",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Reference_no",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Shared",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Sold_share",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Warehouse_code",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Warehouse_desc",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "Onway",
                table: "Inventories",
                newName: "OnWay");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
