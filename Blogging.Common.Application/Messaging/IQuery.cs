using Blogging.Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Application.Messaging
{
    public interface BaseQuery;
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>, BaseQuery;
}
