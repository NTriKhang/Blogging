using Blogging.Common.Domain;

namespace Blogging.Common.Application.EventBus
{
    public interface IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
    {
        Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
    }
}
