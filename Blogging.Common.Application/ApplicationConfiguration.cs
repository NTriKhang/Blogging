using Blogging.Common.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Blogging.Common.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services
            , Assembly[] assemblies)
        {
            services.AddMediatR(option =>
            {
                option.RegisterServicesFromAssemblies(assemblies);

                option.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

            return services;
        }
    }
}
