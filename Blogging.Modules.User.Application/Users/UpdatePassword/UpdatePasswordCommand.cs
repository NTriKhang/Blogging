using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Application.Users.UpdatePassword
{
    public sealed record UpdatePasswordCommand(
        Guid UserId
        , string Password) : ICommand;
}
