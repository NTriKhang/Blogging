using Blogging.Common.Application.Data;
using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.Inbox;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal sealed class IdempotencyIntegrationEventHandler<TIntegrationEvent>(
        IIntegrationEventHandler<TIntegrationEvent> decorater
        , IDbConnectionFactory dbConnectionFactory)
        : IntegrationEventHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        public override async Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            InboxMessageConsumer consumer = new InboxMessageConsumer(integrationEvent.Id, decorater.GetType().Name);
            if(await InboxConsumerExistAsync(dbConnection, consumer))
            {
                return;
            }
            await decorater.Handle(integrationEvent, cancellationToken);

            await InsertInboxConsumerAsync(dbConnection, consumer);
        }
        private static async Task InsertInboxConsumerAsync(
        DbConnection dbConnection
        , InboxMessageConsumer consumer)
        {
            try
            {
                const string sql =
                $"""
                INSERT INTO "{Schemas.Blog}"."InboxMessageConsumer"("Id", "Name")
                VALUES (@Id, @Name)
                """;

                await dbConnection.ExecuteAsync(sql, consumer);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static async Task<bool> InboxConsumerExistAsync(
            DbConnection dbConnection
            , InboxMessageConsumer consumer)
        {
            string sql =
            $"""
                SELECT EXISTS (
                    SELECT 1
                    FROM "{Schemas.Blog}"."InboxMessageConsumer"
                    WHERE "Id" = @Id AND "Name" = @Name
                )
                """;
            return await dbConnection.ExecuteScalarAsync<bool>(sql, consumer);
        }
    }
}
