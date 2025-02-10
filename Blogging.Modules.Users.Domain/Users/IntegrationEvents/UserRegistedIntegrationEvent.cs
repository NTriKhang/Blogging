using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users.IntegrationEvents
{
    public sealed class UserRegistedIntegrationEvent : IntegrationEvent
    {
        public UserRegistedIntegrationEvent(Guid id
            , DateTime occuredOnUtc,
            Guid userId ,
            string userName,
            string email,
            string displayName,
            string imageUrl) : base(id, occuredOnUtc)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            DisplayName = displayName;
            ImageUrl = imageUrl;
        }
        public Guid UserId { get; init; }
        public string UserName { get; init; }   
        public string Email { get; init; }
        public string DisplayName { get; init; }
        public string ImageUrl { get; init; }
    }
}
