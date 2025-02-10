using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogging.Modules.User.Domain.Users;
using Blogging.Modules.User.Application.Users.GetUser;
using Blogging.Common.Domain;
using MassTransit;
using Blogging.Common.Application.EventBus;
using Blogging.Modules.User.Domain.Users.IntegrationEvents;

namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    internal sealed class UserRegisterDomainEventHandler(
        ISender sender
        , IEventBus eventBus 
        ) : INotificationHandler<UserRegisterDomainEvent>
    {
        public async Task Handle(UserRegisterDomainEvent notification, CancellationToken cancellationToken)
        {
            Result<GetUserResponse> user = await sender.Send(new GetUserQuery(notification.UserId));
            if (user.IsFailure)
                throw new Exception($"{nameof(GetUserQuery)} cause Exception: " +
                    $"{user.Error.Description}");

            await eventBus.PublishAsync(new UserRegistedIntegrationEvent(
                notification.Id
                , notification.OccurredAtUtc
                , user.Value.Id
                , user.Value.UserName
                , user.Value.Email
                , user.Value.DisplayName
                , user.Value.ImageUrl));
        }
    }
}
