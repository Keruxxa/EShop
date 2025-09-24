using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(image => image.Id);

        builder
            .HasOne<Product>()
            .WithMany(product => product.Images)
            .HasForeignKey(image => image.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
