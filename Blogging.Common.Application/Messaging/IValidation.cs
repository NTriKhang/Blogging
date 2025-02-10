using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Application.Messaging
{
    public interface IValidation<in TRequest> 
        where TRequest : ICommand
    {
        ValueTask<Result> ValidateAsync(TRequest request);
    };
    public interface IValidation<in TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    {
        ValueTask<Result> ValidateAsync(TRequest request);
    };
}
