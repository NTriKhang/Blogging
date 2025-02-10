using Blogging.Common.Application.Messaging;


namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(RegisterUserDto request) : ICommand<Guid>;
}
