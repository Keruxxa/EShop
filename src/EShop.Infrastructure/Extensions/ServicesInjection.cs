using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IBasketService, BasketService>();
        services.AddTransient<IBrandService, BrandService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IRoleTypeService, RoleTypeService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IOrderService, OrderService>();

        services.AddTransient<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
