using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.User.Application.Abtractions.Data;
using Blogging.Modules.User.Application.Abtractions.Identity;
using Blogging.Modules.User.Domain.Users;
using Blogging.Modules.User.Infrastructure.Database;
using Blogging.Modules.User.Infrastructure.Identity;
using Blogging.Modules.User.Infrastructure.Outbox;
using Blogging.Modules.User.Infrastructure.Users;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scrutor;


namespace Blogging.Modules.User.Infrastructure
{
    public static class UserModule
    {
        public static IServiceCollection AddUsersModule(
            this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            return services;
        }
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityProviderService, IdentityProviderService>();

            services.Configure<KeyCloakOptions>(configuration.GetSection("KeyCloak"));
            services.AddTransient<KeyCloakAuthDelegatingHandler>();

            services.AddHttpClient<KeyCloakClient>((sp, httpClient) =>
            {
                KeyCloakOptions options = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
                httpClient.BaseAddress = new Uri(options.AdminUrl);
            }).AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

            services.AddDbContext<UserDbContext>((sp, op) =>
                op
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesinterceptor>())
            );
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.ConfigureOptions<ConfigureProcessOutboxJob>();

            services.Scan(scan =>
            {
                scan.FromAssemblies([Blogging.Modules.User.Application.AssemblyReference.Assembly])
                .RegisterHandlers(typeof(INotificationHandler<>));
            });

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        }
        public static IImplementationTypeSelector RegisterHandlers(this IImplementationTypeSelector selector, Type type)
        {
            return selector.AddClasses(c =>
                    c.AssignableTo(type)
                        .Where(t => t != typeof(IdempotentDomainEventHandler<>))
                )
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        }
    }
}
