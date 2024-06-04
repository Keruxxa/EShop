using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(brand => brand.Id);

            builder.Property(brand => brand.Id)
                .IsRequired();

            builder.Property(brand => brand.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasMany(brand => brand.Products)
                .WithOne(product => product.Brand)
                .HasForeignKey(product => product.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
