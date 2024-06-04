using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(basketItem => basketItem.Id);

            builder.Property(basketItem => basketItem.Id)
                .IsRequired();

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
}
