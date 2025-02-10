using Microsoft.AspNetCore.Authorization;

namespace Blogging.Common.Infrastructure.Authorizations
{
    internal class PermissionRequirements : IAuthorizationRequirement
    {
        public string Permission { get; }
        public PermissionRequirements(string permission)
        {
            Permission = permission;
        }
    }
}
