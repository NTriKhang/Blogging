namespace Blogging.Common.Application.EventBus
{
    public class IntegrationEvent : IIntegrationEvent
    {
        public IntegrationEvent(Guid id, DateTime occuredOnUtc)
        {
            Id = id;
            OccuredOnUtc = occuredOnUtc;
        }
        public Guid Id { get; init; }

        public DateTime OccuredOnUtc { get; init; }
    }
}
