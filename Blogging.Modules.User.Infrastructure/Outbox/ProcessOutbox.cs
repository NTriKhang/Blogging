using Blogging.Common.Application.Data;
using Blogging.Common.Domain;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Common.Infrastructure.Serialization;
using Dapper;
using MassTransit.Configuration;
using MassTransit.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Outbox
{
    internal sealed record OutboxMessageResponse(Guid Id, string Content);
    internal sealed class ProcessOutbox(IDbConnectionFactory dbConnectionFactory
        , IServiceScopeFactory serviceScopeFactory
        , IOptions<OutboxOptions> outboxOptions) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
            using DbTransaction transaction = await connection.BeginTransactionAsync();

            var outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

            foreach(OutboxMessageResponse outboxMessage in outboxMessages)
            {
                Exception? exception = null;
                try
                {
                    IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content,
                        SerializerSetting.Instances)!;

                    using IServiceScope scope = serviceScopeFactory.CreateScope();
                    IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                    await publisher.Publish(domainEvent);
                }
                catch (Exception caughtException)
                {
                    exception = caughtException;
                }
                await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
            }

            await transaction.CommitAsync();
        }
        private async Task UpdateOutboxMessageAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            OutboxMessageResponse outboxMessage,
            Exception? exception)
        {
            const string sql =
                """
            UPDATE "users"."OutboxMessage"
            SET "ProcessedOnUtc" = @ProcessedOnUtc,
                "Error" = @Error
            WHERE "Id" = @Id
            """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    outboxMessage.Id,
                    ProcessedOnUtc = DateTime.UtcNow,
                    Error = exception?.ToString()
                },
                transaction: transaction);
        }
        private async Task<IReadOnlyCollection<OutboxMessageResponse>> GetOutboxMessagesAsync(
            IDbConnection dbConnection
            , IDbTransaction transaction)
        {
            string sql =
            $"""
             SELECT
                "Id" AS {nameof(OutboxMessageResponse.Id)},
                "Content" AS {nameof(OutboxMessageResponse.Content)}
             FROM "users"."OutboxMessage"
             WHERE "ProcessedOnUtc" IS NULL
             ORDER BY "OccuredOnUtc"
             LIMIT {outboxOptions.Value.BatchSize}
             FOR UPDATE
             """;
            IEnumerable<OutboxMessageResponse> res = await dbConnection.QueryAsync<OutboxMessageResponse>(sql
                , transaction: transaction);
            return res.ToList();
        } 
    }
}
