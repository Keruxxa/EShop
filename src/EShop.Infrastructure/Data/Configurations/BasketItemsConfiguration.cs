using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class BasketItemsConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems");

        builder.HasKey(basketItem => new
        {
            basketItem.BasketId,
            basketItem.ProductId
        });

        builder.HasOne<Basket>()
            .WithMany(basket => basket.BasketItems)
            .HasForeignKey(basketItem => basketItem.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(basketItem => basketItem.Product)
            .WithMany()
            .HasForeignKey(basketItem => basketItem.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
