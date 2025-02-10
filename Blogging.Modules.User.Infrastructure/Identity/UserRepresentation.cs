namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal record UserRepresentation(
        string username
        , string email
        , bool emailVerified
        , bool enabled
        , CredentialRepresentation[] credentials);
}
