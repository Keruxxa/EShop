using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(favorite => favorite.Id);

        builder.Ignore(favorite => favorite.FavoriteProducts);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Favorite>(favorite => favorite.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(favoriteProduct => favoriteProduct.FavoriteProducts)
            .WithOne()
            .HasForeignKey(favoriteProducts => favoriteProducts.FavoriteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
