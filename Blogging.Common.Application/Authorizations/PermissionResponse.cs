namespace Blogging.Common.Application.Authorizations
{
    public sealed record PermissionResponse(Guid UserId, HashSet<string> Permissions);
}
