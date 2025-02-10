using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users.IntegrationEvents
{
    public class UserProfileUpdatedIntegrationEvent : IntegrationEvent
    {
        public UserProfileUpdatedIntegrationEvent(Guid id
            , DateTime occuredOnUtc
            , Guid userId
            , string userName
            , string displayName
            , string email
            , string imageUrl) : base(id, occuredOnUtc)
        {
            UserId = userId;
            UserName = userName;
            DisplayName = displayName;
            Email = email;
            ImageUrl = imageUrl;
        }
        public Guid UserId { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string DisplayName { get; init; }
        public string ImageUrl { get; init; }
    }
}
