using Blogging.Common.Domain;

namespace Blogging.Modules.User.Application.Abtractions.Identity
{
    public static class IdentityProviderErrors
    {
        public static readonly Error EmailIsNotUnique = Error.Conflict(
            "IdentityProvider.EmailIsNotUnique",
            "The specified email is not unique"
        );
    }
}
