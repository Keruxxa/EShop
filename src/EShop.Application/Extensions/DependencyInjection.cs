using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
