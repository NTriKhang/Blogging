using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Users;
using Blogging.Modules.Blog.Infrastructure.Database;
using Blogging.Modules.Blog.Infrastructure.Inbox;
using Blogging.Modules.Blog.Infrastructure.Users;
using Blogging.Modules.User.IntegrationEvent;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure
{
    public static class BlogModule
    {
        public static IServiceCollection AddBlogModule(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddIntegrationEventHandler();

            services.AddInfrastructure(configuration);

            return services;
        }
        public static void AddConsumers(IRegistrationConfigurator conf)
        {
            conf.AddConsumer<IntegrationEventConsumer<UserRegistedIntegrationEvent>>();
            conf.AddConsumer<IntegrationEventConsumer<UserProfileUpdatedIntegrationEvent>>();
        }
        public static void AddIntegrationEventHandler(this IServiceCollection services)
        {
            var integrationEventHandlers = Presentation.AssemblyReference.Assembly.GetTypes()
               .Where(type => type.IsAssignableTo(typeof(IIntegrationEventHandler)))
               .ToArray();

            foreach (var handler in integrationEventHandlers)
            {
                services.TryAddScoped(handler);

                Type integrationEvent = handler
                    .GetInterfaces()
                    .Single(i => i.IsGenericType)
                    .GetGenericArguments()
                    .Single();

                Type idempotencyHandler = typeof(IdempotencyIntegrationEventHandler<>)
                    .MakeGenericType(integrationEvent);

                services.Decorate(handler, idempotencyHandler);
            }
        }
        public static void AddInfrastructure(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>((sp, op) =>
                op.
                UseNpgsql(configuration.GetConnectionString("Database"))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesinterceptor>())
            );

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BlogDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<InboxOptions>(configuration.GetSection("Inbox"));
            services.ConfigureOptions<ConfigureProcessInboxJob>();
        }
    }
}
