using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Data;

public class EShopDbContext : DbContext, IEShopDbContext
{
    public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CategoryProducts> CategoryProducts { get; set; }
    public DbSet<FavoriteProducts> FavoriteProducts { get; set; }
    public DbSet<BrandProducts> BrandProducts { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketsItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EShopDbContext).Assembly);
    }
}
