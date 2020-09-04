﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WholesaleSystem.Models;

namespace WholesaleSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WholesaleSystem.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Onway")
                        .HasColumnType("int");

                    b.Property<int>("Pending")
                        .HasColumnType("int");

                    b.Property<DateTime>("Pi_update_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Product_barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_sku")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_title_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference_no")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Reserved")
                        .HasColumnType("int");

                    b.Property<int>("Sellable")
                        .HasColumnType("int");

                    b.Property<int>("Shared")
                        .HasColumnType("int");

                    b.Property<int>("Shipped")
                        .HasColumnType("int");

                    b.Property<int>("Sold_share")
                        .HasColumnType("int");

                    b.Property<int>("Unsellable")
                        .HasColumnType("int");

                    b.Property<string>("Warehouse_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Warehouse_desc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("WholesaleSystem.Models.OperationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("OperatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PicturePathId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("PicturePathId");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("WholesaleSystem.Models.PicturePath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMainPicture")
                        .HasColumnType("bit");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("PicturePaths");
                });

            modelBuilder.Entity("WholesaleSystem.Models.OperationLog", b =>
                {
                    b.HasOne("WholesaleSystem.Models.Inventory", null)
                        .WithMany("OperationLogs")
                        .HasForeignKey("InventoryId");

                    b.HasOne("WholesaleSystem.Models.PicturePath", null)
                        .WithMany("OperationLogs")
                        .HasForeignKey("PicturePathId");
                });

            modelBuilder.Entity("WholesaleSystem.Models.PicturePath", b =>
                {
                    b.HasOne("WholesaleSystem.Models.Inventory", "Inventory")
                        .WithMany("PicturePaths")
                        .HasForeignKey("InventoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
