using EShop.Infrastructure.Utilities.Mapping;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class MapsterInjection
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        services.AddTransient<IMapper, Mapper>();
        MapsterConfig.ConfigureMapping();

        return services;
    }
}
