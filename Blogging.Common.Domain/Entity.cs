using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Domain
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

        protected Entity()
        {
            
        }
        public void ClearDomain()
        {
            _domainEvents.Clear();
        }
        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
