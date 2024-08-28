using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EShop.Infrastructure.Extensions;

public static class JwtAuthentication
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var validIssuer = configuration.GetValue<string>("Issuer");
        var validAudience = configuration.GetValue<string>("Audience");
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Key")));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidateLifetime = true,
                    IssuerSigningKey = issuerSigningKey,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role
                };

                // This should be removed but it's not yet because it's used to check authorization
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["eshop-server-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
