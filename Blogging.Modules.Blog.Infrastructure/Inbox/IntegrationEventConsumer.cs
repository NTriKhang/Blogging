using Blogging.Common.Application.Data;
using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.Inbox;
using Blogging.Common.Infrastructure.Serialization;
using Blogging.Modules.Blog.Infrastructure.Database;
using Dapper;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
        : IConsumer<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        public async Task Consume(ConsumeContext<TIntegrationEvent> context)
        {
            using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
            TIntegrationEvent integrationEvent = context.Message;
            var inboxMessage = new InboxMessage(integrationEvent.Id
                , integrationEvent.GetType().Name
                , JsonConvert.SerializeObject(integrationEvent, SerializerSetting.Instances)
                , integrationEvent.OccuredOnUtc);

            const string sql =
            $"""
            INSERT INTO "{Schemas.Blog}"."InboxMessage"("Id", "Type", "Content", "OccuredOnUtc")
            VALUES (@Id, @Type, @Content::json, @OccuredOnUtc)
            """;

            await connection.ExecuteAsync(sql, inboxMessage);
        }
    }
}
