using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blogging.Common.Infrastructure.Inbox
{
    public sealed class InboxMessageConsumer(Guid id, string name)
    {
        public Guid Id { get; init; } = id;
        public string Name { get; init; } = name;
    }
}
