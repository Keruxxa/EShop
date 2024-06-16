using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations
{
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

            builder.HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<CategoryProducts>()
                .WithOne()
                .HasForeignKey(categoryProducts => categoryProducts.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
