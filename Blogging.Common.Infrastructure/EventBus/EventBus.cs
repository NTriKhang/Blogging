using Blogging.Common.Application.EventBus;
using Blogging.Common.Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.EventBus
{
    internal class EventBus(IBus bus) : IEventBus
    {
        public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : IIntegrationEvent
        {
            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
