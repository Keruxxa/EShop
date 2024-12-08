using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Id)
            .IsRequired();

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasIndex(category => category.Name)
            .IsUnique();

        builder.HasMany(category => category.CategoryProducts)
            .WithOne()
            .HasForeignKey(categoryProducts => categoryProducts.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<CategoryClosureNode>()
            .WithOne(categoryClosureNode => categoryClosureNode.AncestorCategory)
            .HasForeignKey(categoryClosureNode => categoryClosureNode.AncestorCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<CategoryClosureNode>()
            .WithOne()
            .HasForeignKey(categoryClosureNode => categoryClosureNode.DescendantCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
