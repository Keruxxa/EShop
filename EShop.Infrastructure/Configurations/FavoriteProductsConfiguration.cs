using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

public class FavoriteProductsConfiguration : IEntityTypeConfiguration<FavoriteProducts>
{
    public void Configure(EntityTypeBuilder<FavoriteProducts> builder)
    {
        builder.HasKey(favoriteProducts => new
        {
            favoriteProducts.FavoriteId,
            favoriteProducts.ProductId
        });
    }
}
