using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public OutboxMessage()
        {

        }
        public OutboxMessage(Guid id
            , string type
            , string content
            , DateTime occuredOnUtc)
        {
            this.Id = id;
            this.Type = type;
            this.Content = content;
            this.OccuredOnUtc = occuredOnUtc;
        }
        public Guid Id { get; init; }
        public string Type { get; init; }
        public string Content { get; init; }
        public DateTime OccuredOnUtc { get; init; }
        public DateTime? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
