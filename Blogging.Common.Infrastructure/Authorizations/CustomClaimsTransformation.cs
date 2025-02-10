using Blogging.Common.Infrastructure.Authentications;
using Blogging.Common.Application.Authorizations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Blogging.Common.Domain;

namespace Blogging.Common.Infrastructure.Authorizations
{
    internal class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.HasClaim(c => c.Type == CustomClaims.Sub))
                return principal;

            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            string identity = principal.GetIdentityId()!;

            Result<PermissionResponse> result = await permissionService.GetUserPermissionAsync(identity);
            if (result.IsFailure)
            {
                throw new Exception(nameof(IPermissionService.GetUserPermissionAsync));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(CustomClaims.Sub, identity));
            foreach(var per in result.Value.Permissions)
            {
                claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, per));
            }
            principal.AddIdentity(claimsIdentity);
            return principal;
        }
    }
}
