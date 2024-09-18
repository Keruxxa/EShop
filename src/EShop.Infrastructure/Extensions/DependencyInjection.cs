using EShop.Application.Interfaces;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrusture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EShopDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("EShopConnectionString"));
        });

        services.AddScoped<IEShopDbContext, EShopDbContext>();

        services.AddRepositories();
        services.AddServices();

        services.AddUtilities();

        services.AddMediatr();
        services.AddMapster();

        services.AddJwtAuthentication(configuration.GetSection("JwtOptions"));

        return services;
    }
}
