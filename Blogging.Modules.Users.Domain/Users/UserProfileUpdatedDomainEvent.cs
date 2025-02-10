using Blogging.Common.Domain;

namespace Blogging.Modules.User.Domain.Users
{
    public sealed class UserProfileUpdatedDomainEvent(Guid userId
        , string displayName
        , string userName
        , string email
        , string imageUrl) : DomainEvent
    {
        public Guid UserId { get; init; } = userId;
        public string DisplayName { get; init; } = displayName;
        public string UserName { get; init; } = userName;
        public string Email { get; init; } = email;
        public string ImageUrl { get; init; } = imageUrl;
    }
}
