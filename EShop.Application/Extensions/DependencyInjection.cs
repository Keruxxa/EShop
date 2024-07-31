using EShop.Application.Behaviors;
using EShop.Application.Mapping;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EShop.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddMapster();
            MapsterConfig.ConfigureMapping();

            services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
