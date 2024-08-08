using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(favorite => favorite.Id);

        builder.Property(favorite => favorite.Id)
            .IsRequired();

        builder.Ignore(favorite => favorite.Products);

        builder.HasMany<FavoriteProducts>()
            .WithOne()
            .HasForeignKey(favoriteProducts => favoriteProducts.FavoriteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
