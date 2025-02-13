using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Users.CreateReader
{
    public sealed record CreateUserCommand(Guid Id
        , string UserName
        , string DisplayName
        , string ImageUrl
        , string Email) : ICommand<Guid>;
}
