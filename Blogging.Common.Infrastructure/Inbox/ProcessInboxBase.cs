using Blogging.Common.Application.Data;
using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.Serialization;
using Dapper;
using MassTransit.Internals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Blogging.Common.Infrastructure.Inbox
{
    public sealed record InboxMessageResponse(Guid Id, string Content);
    public class ProcessInboxBase(IOptions<InboxOptionsBase> inboxOptions
        , IDbConnectionFactory dbConnectionFactory
        , IServiceScopeFactory serviceScopeFactory
        , Assembly assembly
        , string schema) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            using DbTransaction transaction = await dbConnection.BeginTransactionAsync();
            var inboxMessage = await GetInboxMessagesAsync(dbConnection, transaction);

            foreach (var message in inboxMessage)
            {
                Exception? exception = null;
                try
                {
                    IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(message.Content
                        , SerializerSetting.Instances)!;
                    using IServiceScope scope = serviceScopeFactory.CreateScope();

                    IEnumerable<IIntegrationEventHandler> handlers = IntegrationEventHandlerFactory
                        .GetHandlers(assembly, integrationEvent.GetType(), scope.ServiceProvider);

                    foreach (IIntegrationEventHandler handler in handlers)
                    {
                        await handler.Handle(integrationEvent, context.CancellationToken);
                    }
                }
                catch (Exception caughtException)
                {
                    exception = caughtException;
                }
                await UpdateInboxMessageAsync(dbConnection, transaction, message, exception);
            }
            await transaction.CommitAsync();
        }
        private async Task UpdateInboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        InboxMessageResponse inboxMessage,
        Exception? exception)
        {
            string sql =
            $"""
            UPDATE "{schema}"."InboxMessage"
            SET "ProcessedOnUtc" = @ProcessedOnUtc,
                "Error" = @Error
            WHERE "Id" = @Id
            """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    inboxMessage.Id,
                    ProcessedOnUtc = DateTime.UtcNow,
                    Error = exception?.ToString()
                },
                transaction: transaction);
        }
        private async Task<IReadOnlyCollection<InboxMessageResponse>> GetInboxMessagesAsync(
        IDbConnection dbConnection
        , IDbTransaction transaction)
        {
            string sql =
            $"""
             SELECT
                "Id" AS {nameof(InboxMessageResponse.Id)},
                "Content" AS {nameof(InboxMessageResponse.Content)}
             FROM "{schema}"."InboxMessage"
             WHERE "ProcessedOnUtc" IS NULL
             ORDER BY "OccuredOnUtc"
             LIMIT {inboxOptions.Value.BatchSize}
             FOR UPDATE
             """;
            IEnumerable<InboxMessageResponse> res = await dbConnection.QueryAsync<InboxMessageResponse>(sql
                , transaction: transaction);
            return res.ToList();
        }
    }
}
