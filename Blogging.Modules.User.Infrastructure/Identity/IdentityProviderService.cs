using Blogging.Common.Domain;
using Blogging.Modules.User.Application.Abtractions.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal class IdentityProviderService(KeyCloakClient keyCloakClient) : IIdentityProviderService
    {
        private const string PasswordCredentialType = "password";
        // {POAST} /admin/realms/{realm}/users
        public async Task<Result<string>> RegisterUserAsync(UserIdentityRegisterDto user
            , CancellationToken cancellationToken = default)
        {
            try
            {
                string identityId = await keyCloakClient.RegisterUserAsync(new UserRepresentation(
                    user.userName
                , user.email
                , true
                , true
                , [new CredentialRepresentation(PasswordCredentialType, user.password, false)]
                ));

                return identityId;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return Result.Failure<string>(Error.Conflict(
                        "IdentityProvider.DataConflict"
                        , ex.Message
                    ));
            }
        }
    }
}
