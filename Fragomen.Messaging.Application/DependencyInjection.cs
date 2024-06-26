using Frogomen.Messaging.Domain.Configurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using Fragomen.Messaging.Application.Common.Behaviors;
using Microsoft.Extensions.Configuration;

namespace Fragomen.Messaging.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConfigurationOptions>(config.GetSection(SectionNames.AWS));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}
