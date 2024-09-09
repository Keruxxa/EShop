using EShop.Infrastructure.Utilities.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShop.Infrastructure.Extensions;

public static class Mediatr
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Mediatr).Assembly);
        });

        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
