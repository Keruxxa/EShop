using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(country => country.Id);

        builder.Property(country => country.Id)
            .IsRequired();

        builder.Property(country => country.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasData([
            new Country("Китай") { Id = 1 },
            new Country("Россия") { Id = 2 },
            new Country("Корея") { Id = 3 },
            new Country("Индия") { Id = 4 },
            new Country("Германия") { Id = 5 },
            new Country("Франция") { Id = 6 }
            ]);
    }
}
