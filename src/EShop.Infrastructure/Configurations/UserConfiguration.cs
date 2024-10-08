﻿using EShop.Domain.Entities;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations;

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

        builder.HasOne(user => user.Role)
            .WithMany()
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
