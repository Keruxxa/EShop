using EShop.Application.Mapping;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapster();
        MapsterConfig.ConfigureMapping();

        return services;
    }
}
