using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Domain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None
                || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result Success() => new(true, Error.None);
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Failure<T>(Error error) => new(default, true, error);
    }
    public class Result<T> : Result
    {
        private readonly T? _value;
        public Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }
        public T Value => IsSuccess ?
            _value! :
            throw new InvalidOperationException("The value of a failure result can't be accessed.");
        public static implicit operator Result<T>(T? value) =>
            value is not null ? Success<T>(value) : Failure<T>(Error.NullValue);

        public static Result<T> ValidationError(Error error)
            => new(default, false, error);
    }
}
