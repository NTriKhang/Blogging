using Blogging.Common.Domain;
using Blogging.Common.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Outbox
{
    public class InsertOutboxMessagesinterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData
            , InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if(eventData.Context is not null)
            {
                var outboxMessages = eventData.Context
                    .ChangeTracker
                    .Entries<Entity>()
                    .Select(entity => entity.Entity)
                    .SelectMany(entity =>
                    {
                        IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
                        entity.ClearDomain();
                        return domainEvents;
                    })
                    .Select(domainEvent =>
                    new OutboxMessage(domainEvent.Id
                        , domainEvent.GetType().Name
                        , JsonConvert.SerializeObject(domainEvent, SerializerSetting.Instances)
                        , DateTime.UtcNow)
                    )
                    .ToList();
                eventData.Context.Set<OutboxMessage>().AddRange(outboxMessages);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
