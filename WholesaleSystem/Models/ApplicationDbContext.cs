using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<OperationLog> OperationLogs { get; set; }

        public DbSet<PicturePath> PicturePaths { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<InventoryProductType> InventoryProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6VTKPLI;Database=WholesaleSystem;Integrated Security=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //一对零关系插入‘零’对象时报错，显示无法插入，原因为无外键暴露。暂时用一对多关系顶替。
            //modelBuilder.Entity<FCRegularLocationDetail>()
            //    .HasOptional(c => c.PickingRecord)
            //    .WithRequired(c => c.FCRegularLocationDetail);

            modelBuilder.Entity<InventoryProductType>()
                .HasKey(x => new { x.InventoryId, x.ProductTypeId });

            modelBuilder.Entity<InventoryProductType>()
                .HasOne(x => x.Inventory)
                .WithMany(x => x.InventoryProductTypes)
                .HasForeignKey(x => x.InventoryId);

            modelBuilder.Entity<InventoryProductType>()
                .HasOne(x => x.ProductType)
                .WithMany(x => x.InventoryProductTypes)
                .HasForeignKey(x => x.ProductTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
