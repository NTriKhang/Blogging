using Blogging.Common.Application.Data;
using Blogging.Common.Domain;
using Blogging.Common.Infrastructure.Outbox;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Outbox
{
    internal class IdempotentDomainEventHandler<TDomainEvent>(
        INotificationHandler<TDomainEvent> decorator
        , IDbConnectionFactory dbConnectionFactory) :
        INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
            var outboxMessageConsumer = new OutboxMessageConsumer(notification.Id, decorator.GetType().Name);

            if(await OutboxConsumerExistAsync(connection, outboxMessageConsumer))
            {
                return;
            }
            await decorator.Handle(notification, cancellationToken);

            await InsertOutboxConsumerAsync(connection, outboxMessageConsumer);
        }
        private static async Task InsertOutboxConsumerAsync(
            DbConnection dbConnection
            , OutboxMessageConsumer consumer)
        {
            try
            {
                const string sql =
                """
                INSERT INTO "users"."OutboxMessageConsumer"("Id", "Name")
                VALUES (@Id, @Name)
                """;

                await dbConnection.ExecuteAsync(sql, consumer);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static async Task<bool> OutboxConsumerExistAsync(
            DbConnection dbConnection
            , OutboxMessageConsumer consumer)
        {
            string sql = 
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM "users"."OutboxMessageConsumer"
                    WHERE "Id" = @Id AND "Name" = @Name
                )
                """;
            return await dbConnection.ExecuteScalarAsync<bool>(sql, consumer);
        }
    }
}
