using BrandApplication.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrandApplication.DataAccess.FluentConfiguration
{
    internal class Brand_FluentConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> modelBuilder)
        {
            modelBuilder
                .HasMany<ProductModel>(b => b.Models)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
