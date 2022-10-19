using LinqAssignmentCore.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LinqAssignmentCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
    }
}
