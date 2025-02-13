using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Users.UpdateUserProfile
{
    public sealed record UpdateUserProfileCommand(
        Guid Id
        , string DisplayName
        , string UserName
        , string Email
        , string ImasgeUrl) : ICommand;
}
