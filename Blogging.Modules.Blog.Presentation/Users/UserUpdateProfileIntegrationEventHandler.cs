using Blogging.Common.Application.EventBus;
using Blogging.Modules.Blog.Application.Users.UpdateUserProfile;
using Blogging.Modules.User.IntegrationEvent;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Users
{
    internal class UserUpdateProfileIntegrationEventHandler(ISender sender) :
        IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
    {
        public override async Task Handle(UserProfileUpdatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            var res = await sender.Send(new UpdateUserProfileCommand(
                integrationEvent.UserId
                , integrationEvent.DisplayName
                , integrationEvent.UserName
                , integrationEvent.Email
                , integrationEvent.ImageUrl));

            if (res.IsFailure)
                Console.WriteLine(res.Error.Description);
        }
    }
}
