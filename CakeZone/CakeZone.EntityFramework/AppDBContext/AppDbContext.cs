using CakeZone.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.EntityFramework.AppDBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<CakeModel> Cakes { get; set; }
        public DbSet<CoverModel> Covers { get; set; }
        public DbSet<FillingModel> Fillings { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

    }
}
