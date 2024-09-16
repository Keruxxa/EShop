using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .IsRequired();

        builder.Property(role => role.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasMany<User>()
            .WithOne(user => user.Role)
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // TODO: сформировать через IEnumerable
        builder.HasData(
        [
            new Role("Администратор") { Id = RoleType.Administrator },
            new Role("Менеджер") { Id = RoleType.Manager },
            new Role("Пользователь") { Id = RoleType.RegisteredUser },
            new Role("Странник") { Id = RoleType.UnregisteredUser }
        ]);

    }
}