using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(review => review.Id);

            builder.Property(review => review.Id)
                .IsRequired();

            builder.Property(review => review.Text)
                .HasMaxLength(2048);

            builder.Property(review => review.Rating)
                .IsRequired();

            builder
                .HasOne<Product>()
                .WithMany(product => product.Reviews)
                .HasForeignKey(review => review.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
