using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .IsRequired();

        builder.Property(user => user.FirstName)
            .HasMaxLength(32);

        builder.Property(user => user.LastName)
            .HasMaxLength(32);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(user => user.Phone)
            .HasMaxLength(11);

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(user => user.RoleId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasDefaultValue(RoleType.UnregisteredUser);

        builder.HasOne<Basket>()
            .WithOne()
            .HasForeignKey<Basket>(basket => basket.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Review>()
            .WithOne()
            .HasForeignKey(review => review.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(user => user.Role)
            .WithMany()
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Order>()
            .WithOne()
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
