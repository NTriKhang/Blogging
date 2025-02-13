using Blogging.Common.Application.Data;
using Blogging.Common.Application.EventBus;
using Blogging.Common.Infrastructure.Serialization;
using Blogging.Modules.Blog.Infrastructure.Database;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using System.Data;
using System.Data.Common;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal sealed record InboxMessageResponse(Guid Id, string Content);
    internal sealed class ProcessInbox(IOptions<InboxOptions> inboxOptions
        , IDbConnectionFactory dbConnectionFactory
        , IServiceScopeFactory serviceScopeFactory) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            using DbTransaction transaction = await dbConnection.BeginTransactionAsync();
            var inboxMessage = await GetInboxMessagesAsync(dbConnection, transaction);

            foreach(var message in inboxMessage)
            {
                Exception? exception = null;
                try
                {
                    IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(message.Content
                        , SerializerSetting.Instances)!;
                    using IServiceScope serviceProvider = serviceScopeFactory.CreateScope();

                    Type[] handlerTypes = Presentation.AssemblyReference.Assembly
                        .GetTypes()
                        .Where(type => type.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEvent.GetType())))
                        .ToArray();

                    List<IIntegrationEventHandler> handlers = [];
                    foreach(Type handlerType in handlerTypes)
                    {
                        var integrationEventHandler = serviceProvider.ServiceProvider.GetRequiredService(handlerType);
                        handlers.Add((integrationEventHandler as IIntegrationEventHandler)!);
                    }

                    foreach(IIntegrationEventHandler handler in handlers)
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
            const string sql =
            $"""
            UPDATE "{Schemas.Blog}"."InboxMessage"
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
             FROM "{Schemas.Blog}"."InboxMessage"
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
