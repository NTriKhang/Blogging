using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Authentications
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            string? userId = principal.FindFirst(CustomClaims.Sub)?.Value;
            return Guid.TryParse(userId, out Guid parsedUserId) ?
                parsedUserId : throw new InvalidDataException("User identifier is unavailabe");
        }
        public static string? GetIdentityId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                throw new InvalidDataException("User identity is unavailable");
        }
        public static HashSet<string> GetPermissions(this ClaimsPrincipal principal)
        {
            IEnumerable<Claim> claims = principal.FindAll(CustomClaims.Permission) ??
                throw new InvalidDataException("Permissions are unavailable");

            return claims.Select(x => x.Value).ToHashSet();
        }
    }
}
