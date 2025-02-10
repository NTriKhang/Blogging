using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredAtUtc = DateTime.UtcNow;
        }
        protected DomainEvent(Guid Id, DateTime OccurredAtUtc)
        {
            this.Id = Id;
            this.OccurredAtUtc = OccurredAtUtc;
        }
        public Guid Id { get; init; }

        public DateTime OccurredAtUtc { get;init; }
    }
}
