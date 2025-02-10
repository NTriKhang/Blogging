using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Outbox
{
    public sealed class OutboxMessageConsumer(Guid id, string name)
    {
        public Guid Id { get; init; } = id;
        public string Name { get; init; } = name;
    }
}
