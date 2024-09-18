using EShop.Infrastructure.Utilities.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class Mapster
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        services.AddMapster();
        MapsterConfig.ConfigureMapping();

        return services;
    }
}
