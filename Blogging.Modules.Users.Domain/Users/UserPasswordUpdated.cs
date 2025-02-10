using Blogging.Common.Domain;

namespace Blogging.Modules.User.Domain.Users
{
    public sealed class UserPasswordUpdated(Guid userId) : DomainEvent
    {
        public Guid UserId { get; init; } = userId;
    }
}
