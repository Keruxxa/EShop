using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class BrandProductsConfiguration : IEntityTypeConfiguration<BrandProducts>
{
    public void Configure(EntityTypeBuilder<BrandProducts> builder)
    {
        builder.HasKey(brandProduct => new
        {
            brandProduct.BrandId,
            brandProduct.ProductId
        });
    }
}
