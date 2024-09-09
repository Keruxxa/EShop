using EShop.Application.Interfaces.Repositories;
using EShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class RepositoriesInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
