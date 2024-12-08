using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class OrderDeliveryStatusConfiguration : IEntityTypeConfiguration<OrderDeliveryStatus>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryStatus> builder)
    {
        builder.HasKey(orderDeliveryStatus => orderDeliveryStatus.Id);

        builder.Property(orderDeliveryStatus => orderDeliveryStatus.Status)
            .IsRequired();

        builder.HasIndex(orderDeliveryStatus => orderDeliveryStatus.Status)
            .IsUnique();

        builder.HasMany<Order>()
            .WithOne(order => order.OrderDeliveryStatus)
            .HasForeignKey(order => order.OrderDeliveryStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
        [
            new OrderDeliveryStatus("Оплачен") { Id = DeliveryStatus.Payed },
            new OrderDeliveryStatus("В пути") { Id = DeliveryStatus.Sent },
            new OrderDeliveryStatus("Получен") { Id = DeliveryStatus.Recieved }
        ]);
    }
}
