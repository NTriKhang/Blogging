using System.Net.Http.Json;

namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal sealed class KeyCloakClient(HttpClient httpClient)
    {
        internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(
                "users"
                , user
                , cancellationToken);

            httpResponse.EnsureSuccessStatusCode();

            return ExtractIdentityIdFromLocationHeader(httpResponse);
        }
        private static string ExtractIdentityIdFromLocationHeader(
            HttpResponseMessage httpResponseMessage)
        {
            const string usersSegmentName = "users/";

            string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

            if (locationHeader is null)
            {
                throw new InvalidOperationException("Location header is null");
            }

            int userSegmentValueIndex = locationHeader.IndexOf(
                usersSegmentName,
                StringComparison.InvariantCultureIgnoreCase);

            string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

            return identityId;
        }
    }
}
