using Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Car entiti üçün DbSet
        public DbSet<Car> Cars { get; set; }

        // Sale entiti üçün DbSet
        public DbSet<Sale> Sales { get; set; }

        // Customer entiti üçün DbSet
        public DbSet<Customer> Customers { get; set; }

        // Gələcəkdə əlavə etmək istəsəniz digər entitilər burada təyin ediləcəkdir.
    }
}
