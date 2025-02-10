using Blogging.Common.Infrastructure.Authentications;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Authorizations
{
    internal class PermissionAuthorizationHandler() : AuthorizationHandler<PermissionRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , PermissionRequirements requirement)
        {
            HashSet<string> permissions = context.User.GetPermissions();
            if (permissions.Contains(requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
