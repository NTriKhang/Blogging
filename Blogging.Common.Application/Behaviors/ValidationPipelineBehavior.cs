using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blogging.Common.Application.Behaviors
{
    internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        public async Task<TResponse> Handle(TRequest request
            , RequestHandlerDelegate<TResponse> next
            , CancellationToken cancellationToken)
        {

                ValidationResult[] validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(request)));

                ValidationFailure[] validationFailures = validationResults.Where(x => !x.IsValid)
                    .SelectMany(x => x.Errors)
                    .ToArray();
            try
            {
                if (validationFailures.Length == 0)
                {
                    return await next();
                }
                if(typeof(TResponse) == typeof(Result))
                {
                    return (TResponse)(object)Result.Failure(Error.Problem(validationFailures[0].ErrorCode, CreateMessageError(validationFailures)));
                }
                else if(typeof(TResponse).IsGenericType
                    && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var genericType = typeof(TResponse).GetGenericArguments()[0];

                    var genericResult =  typeof(Result<>)
                        .MakeGenericType(genericType)
                        .GetMethod(nameof(Result<object>.ValidationError))!
                        .Invoke(null, [Error.Problem(validationFailures[0].ErrorCode, CreateMessageError(validationFailures))]);
                    
                    return (TResponse)genericResult;
                }
                return (TResponse)(object)Result.Failure(Error.Problem(validationFailures[0].ErrorCode, CreateMessageError(validationFailures)));

            }
            catch (Exception )
            {
                throw;
            }
        }
        private string CreateMessageError(ValidationFailure[] validationFailures)
        {
            return string.Join(Environment.NewLine, validationFailures.Select(f => f.ErrorMessage));
        }
    }
}
