using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.User.Domain.Users;


namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandDataValidator(
        IUserRepository userRepository) : IValidation<RegisterUserCommand, Guid>
    {
        public async ValueTask<Result> ValidateAsync(RegisterUserCommand request)
        {
            Domain.Users.User? user = await userRepository.GetByUserNameAsync(request.request.UserName);
            if (user is not null)
                return Result.Failure(Error.Conflict("Users.Conflict", "This user name already exist"));
            return Result.Success();
        }
    }
}
