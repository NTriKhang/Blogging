using Blogging.Common.Application.Data;
using Blogging.Common.Domain;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Outbox
{
    public class IdempotentDomainEventHandlerBase<TDomainEvent>(
        IDbConnectionFactory dbConnectionFactory
        , INotificationHandler<TDomainEvent> decorator
        , string schema)
        : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            var outboxMessageConsumer = new OutboxMessageConsumer(notification.Id, decorator.GetType().Name);

            if(await OutboxConsumerExistAsync(dbConnection, outboxMessageConsumer, schema))
            {
                return;
            }
            await decorator.Handle(notification, cancellationToken);
            await InsertOutboxConsumerAsync(dbConnection, outboxMessageConsumer, schema);
        }
        private static async Task InsertOutboxConsumerAsync(
            DbConnection dbConnection
            , OutboxMessageConsumer consumer
            , string schema)
        {
            try
            {
                string sql = $"""
                INSERT INTO "{schema}"."OutboxMessageConsumer"("Id", "Name")
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
            , OutboxMessageConsumer consumer
            , string schema)
        {
            string sql =
                $"""
                SELECT EXISTS (
                    SELECT 1
                    FROM "{schema}"."OutboxMessageConsumer"
                    WHERE "Id" = @Id AND "Name" = @Name
                )
                """;
            return await dbConnection.ExecuteScalarAsync<bool>(sql, consumer);
        }
    }
}
