using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .IsRequired();

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(product => product.Description)
            .HasMaxLength(256);

        builder.Ignore(product => product.ReviewCount);

        builder
            .HasMany(product => product.Reviews)
            .WithOne()
            .HasForeignKey(review => review.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(product => product.CountryManufacturer)
            .WithMany()
            .HasForeignKey(product => product.CountryManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<FavoriteProducts>()
            .WithOne()
            .HasForeignKey(favoriteProducts => favoriteProducts.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<CategoryProducts>()
            .WithOne()
            .HasForeignKey(categoryProducts => categoryProducts.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<BrandProducts>()
            .WithOne()
            .HasForeignKey(brandProduct => brandProduct.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
