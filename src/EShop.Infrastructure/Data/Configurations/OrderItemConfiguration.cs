using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.Count)
            .IsRequired();

        builder.HasOne(orderItem => orderItem.Product)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Order>()
            .WithMany(order => order.OrderItems)
            .HasForeignKey(orderItem => orderItem.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
