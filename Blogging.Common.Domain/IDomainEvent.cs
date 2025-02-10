using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Domain
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredAtUtc { get; }
    }
}
