using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound(Guid UserId) =>
            Error.NotFound("Users.NotFound", $"The user with the identifier {UserId} not found");

    }
}
