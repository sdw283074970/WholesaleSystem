using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WholesaleSystem.Models;

namespace WholesaleSystem.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6VTKPLI;Database=WholesaleSystem;Integrated Security=True;MultipleActiveResultSets=true");

            //return new ApplicationDbContext(optionsBuilder.Options);
            return new ApplicationDbContext();
        }
    }
}
