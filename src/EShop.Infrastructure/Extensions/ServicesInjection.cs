using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<IRoleTypeService, RoleTypeService>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<IBrandService, BrandService>();

        services.AddTransient<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
