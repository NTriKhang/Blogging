using Blogging.Common.Application.Data;
using Blogging.Common.Domain;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Common.Infrastructure.Serialization;
using Dapper;
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

namespace Blogging.Common.Infrastructure.Outbox
{
    public class ProcessOutboxBase<TOutboxOptions>(
        IDbConnectionFactory dbConnectionFactory
        , IServiceScopeFactory serviceScopeFactory
        , IOptions<TOutboxOptions> outboxOptions
        , string schema)
        : IJob
        where TOutboxOptions : OutboxOptionsBase
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            using DbTransaction transaction = await dbConnection.BeginTransactionAsync();

            var outboxMessages = await GetOutboxMessagesAsync(dbConnection, transaction);
            Exception? cauthException = null;
            foreach (var outboxMessage in outboxMessages)
            {
                Exception? caughtException = null;
                try
                {
                    IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content
                        , SerializerSetting.Instances)!;

                    using var scope = serviceScopeFactory.CreateScope();
                    var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                    await publisher.Publish(domainEvent);
                }
                catch (Exception ex)
                {
                    cauthException = ex;
                    Console.WriteLine(ex.Message);
                }
                await UpdateOutboxMessageAsync(dbConnection, transaction, outboxMessage, cauthException);
            }
            await transaction.CommitAsync();
        }
           
        private async Task UpdateOutboxMessageAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            OutboxMessageResponse outboxMessage,
            Exception? exception)
        {
            string sql =
                $"""
            UPDATE "{schema}"."OutboxMessage"
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
             FROM "{schema}"."OutboxMessage"
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
