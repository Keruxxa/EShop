using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Configurations
{
    public class CategoryProductsConfigurations : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(categoryProducts => new
            {
                categoryProducts.CategoryId,
                categoryProducts.ProductId
            });
        }
    }
}
