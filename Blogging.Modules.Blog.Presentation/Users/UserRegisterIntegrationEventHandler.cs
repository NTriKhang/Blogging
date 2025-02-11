using Blogging.Common.Application.EventBus;
using Blogging.Modules.Blog.Application.Users.CreateReader;
using Blogging.Modules.User.IntegrationEvent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Users
{
    internal class UserRegisterIntegrationEventHandler(ISender sender) : IntegrationEventHandler<UserRegistedIntegrationEvent>
    {
        public override async Task Handle(UserRegistedIntegrationEvent integrationEvent
            , CancellationToken cancellationToken = default)
        {
            var createCreaderCommand = new CreateReaderCommand(integrationEvent.UserId
                , integrationEvent.UserName
                , integrationEvent.DisplayName
                , integrationEvent.ImageUrl
                , integrationEvent.Email);

            var res = await sender.Send(createCreaderCommand);
            if (res.IsFailure)
                Console.WriteLine(res.Error.Description);
        }
    }
}
