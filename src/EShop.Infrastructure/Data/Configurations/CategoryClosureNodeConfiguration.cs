using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class CategoryClosureNodeConfiguration : IEntityTypeConfiguration<CategoryClosureNode>
{
    public void Configure(EntityTypeBuilder<CategoryClosureNode> builder)
    {
        builder.ToTable("CategoryClosureNodes");

        builder.HasKey(categoryClosureNode => new
        {
            categoryClosureNode.AncestorCategoryId,
            categoryClosureNode.DescendantCategoryId
        });
    }
}
