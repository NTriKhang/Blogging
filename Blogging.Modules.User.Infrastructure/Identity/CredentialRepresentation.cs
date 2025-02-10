namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal record CredentialRepresentation(string type
        , string value
        , bool temporary);
}
