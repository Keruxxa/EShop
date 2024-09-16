using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(favorite => favorite.UserId)
            .HasName("Id");

        builder.HasIndex(favorite => favorite.UserId)
            .IsUnique();

        builder.Ignore(favorite => favorite.Products);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Favorite>(favorite => favorite.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<FavoriteProducts>()
            .WithOne()
            .HasForeignKey(favoriteProducts => favoriteProducts.FavoriteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
