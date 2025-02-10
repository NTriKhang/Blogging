using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Application.Users.UpdateProfile
{
    public sealed record UpdateProfileCommand(Guid UserId
        , string DisplayName
        , string UserName
        , string Email
        , string ImageUrl) : ICommand;
}
