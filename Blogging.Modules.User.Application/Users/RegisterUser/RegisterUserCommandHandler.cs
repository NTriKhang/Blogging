using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.User.Application.Abtractions.Data;
using Blogging.Modules.User.Application.Abtractions.Identity;
using Blogging.Modules.User.Domain.Users;
using FluentValidation;


namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler(
        IIdentityProviderService identityProvider
        , IUserRepository userRepository
        , IUnitOfWork unitOfWork
        , IValidator<RegisterUserCommand> validator) : ICommandHandler<RegisterUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            RegisterUserDto registerUser = request.request;
            Result<string> userIdentity = await identityProvider
                .RegisterUserAsync(new UserIdentityRegisterDto
                ( registerUser.UserName, registerUser.Email, registerUser.Password)
                , cancellationToken );

            if (userIdentity.IsFailure)
                return Result.Failure<Guid>(userIdentity.Error);

            var user = Domain.Users.User.Create(
                Guid.NewGuid()
                , userIdentity.Value
                , registerUser.UserName
                , registerUser.DisplayName
                , registerUser.Email
                , registerUser.Password
                , registerUser.ImageUrl);
            
            userRepository.Insert(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
