using Blogging.Common.Infrastructure.Authentications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blogging.Common.Application;
using Blogging.Common.Infrastructure.Authorizations;
using Blogging.Common.Application.Data;
using Blogging.Common.Infrastructure.Data;
using Npgsql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.EventBus;
using Blogging.Common.Infrastructure.Outbox;
using MassTransit;
using Quartz;

namespace Blogging.Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services
            , Action<IRegistrationConfigurator>[] moduleConfigureConsumers
            , string dbConnectionString)
        {
            services.AddAuthenticationInternal();

            services.AddAuthorizationInternal();

            services.ConfigureOptions<JwtBearerConfigurateOptions>();
            
            NpgsqlDataSource npgsqlData = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
            services.TryAddSingleton(npgsqlData);


            services.AddMassTransit(configure =>
            {
                foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
                {
                    configureConsumers(configure);
                }

                configure.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            services.TryAddSingleton<InsertOutboxMessagesinterceptor>();

            services.TryAddSingleton<IEventBus, EventBus.EventBus>();

            services.AddQuartz();
            services.AddQuartzHostedService(config => config.WaitForJobsToComplete = true);

            return services;
        }
    }
}
