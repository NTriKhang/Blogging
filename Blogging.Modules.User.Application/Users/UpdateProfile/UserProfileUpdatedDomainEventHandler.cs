using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogging.Modules.User.Domain.Users;
using Blogging.Modules.User.Domain.Users.IntegrationEvents;
using Blogging.Common.Application.EventBus;

namespace Blogging.Modules.User.Application.Users.UpdateProfile
{
    internal class UserProfileUpdatedDomainEventHandler(ISender sender
        , IEventBus eventBus) : INotificationHandler<UserProfileUpdatedDomainEvent>
    {
        public async Task Handle(UserProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await eventBus.PublishAsync(new UserProfileUpdatedIntegrationEvent(
                notification.Id
                , notification.OccurredAtUtc
                , notification.UserId
                , notification.UserName
                , notification.DisplayName
                , notification.Email
                , notification.ImageUrl));
        }
    }
}
