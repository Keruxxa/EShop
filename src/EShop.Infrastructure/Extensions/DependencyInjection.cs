﻿using EShop.Application.Interfaces;
using EShop.Application.Interfaces.Security;
using EShop.Application.Interfaces.Services;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Services;
using EShop.Infrastructure.Utilities.Security;
using MapsterMapper;
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

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleTypeService, RoleTypeService>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        services.AddRepositories();

        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IMapper, Mapper>();

        services.AddMediatr();

        services.AddJwtAuthentication(configuration.GetSection("JwtOptions"));

        return services;
    }
}
