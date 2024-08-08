using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

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

        builder.Ignore(brand => brand.BrandProducts);

        builder.HasMany(brand => brand.BrandProducts)
            .WithOne()
            .HasForeignKey(brandProduct => brandProduct.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
