using EShop.Application.Interfaces.Security;
using EShop.Infrastructure.Utilities.Security;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class UtilitiesInjection
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
