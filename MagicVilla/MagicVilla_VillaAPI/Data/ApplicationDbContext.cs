using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa() { Id = 1, Amenity = "Pool", CreatedDate = System.DateTime.Now, Details = "This is a 3 bedroom villa", ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", Name = "Villa 1", Occupancy = 6, Rate = 1000, Sqft = 2000, UpdatedDate = System.DateTime.Now },
                new Villa() { Id = 2, Amenity = "Pool", CreatedDate = System.DateTime.Now, Details = "This is a 4 bedroom villa", ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", Name = "Villa 2", Occupancy = 8, Rate = 2000, Sqft = 3000, UpdatedDate = System.DateTime.Now },
                new Villa() { Id = 3, Amenity = "Pool", CreatedDate = System.DateTime.Now, Details = "This is a 5 bedroom villa", ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg", Name = "Villa 3", Occupancy = 10, Rate = 3000, Sqft = 4000, UpdatedDate = System.DateTime.Now }
                );
        }
    }
}
