using BrandApplication.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BrandApplication.DataAccess
{
    public class BrandDbContext : DbContext
    {
        public BrandDbContext(DbContextOptions<BrandDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductModel> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FluentConfiguration.Brand_FluentConfiguration());
            modelBuilder.Entity<Brand>().HasData(new Brand { BrandId = 1, BrandName = "Brand 1" });
        }   
    }
}
