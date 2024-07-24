using EShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrusture(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EShopDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IEShopDbContext, EShopDbContext>();

            return services;
        }
    }
}
