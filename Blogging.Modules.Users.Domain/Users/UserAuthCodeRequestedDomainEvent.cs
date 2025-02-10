using Blogging.Common.Domain;

namespace Blogging.Modules.User.Domain.Users
{
    internal sealed class UserAuthCodeRequestedDomainEvent(Guid userId
        , string email) : DomainEvent
    {
        public Guid UserId { get; init; } = userId;
        public string Email { get; init; } = email;
    }
}
