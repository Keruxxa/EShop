using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(basket => basket.UserId)
            .HasName("Id");

        builder.HasIndex(basket => basket.UserId)
            .IsUnique();

        builder.HasMany(basket => basket.BasketItems)
            .WithOne()
            .HasForeignKey(basketItem => basketItem.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Basket>(basket => basket.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
