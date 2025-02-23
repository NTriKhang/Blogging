using Blogging.Common.Application.Data;
using Blogging.Common.Domain;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Infrastructure.Database;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Outbox
{
    internal sealed class IdempotentDomainEventHandler<TDomainEvent> : IdempotentDomainEventHandlerBase<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public IdempotentDomainEventHandler(
            INotificationHandler<TDomainEvent> decorator
        , IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory, decorator, Schemas.Blog)
        {
        }
    }
}
