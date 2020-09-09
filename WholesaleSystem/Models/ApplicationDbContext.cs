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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6VTKPLI;Database=WholesaleSystem;Integrated Security=True;MultipleActiveResultSets=true");
        }
    }
}
