using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Blogging.Modules.User.Infrastructure.Identity
{
    internal sealed class KeyCloakAuthDelegatingHandler(IOptions<KeyCloakOptions> options) : DelegatingHandler
    {
        private readonly KeyCloakOptions _options = options.Value;
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request
            , CancellationToken cancellationToken)
        {
            var authCode = await GetAuthorizationToken(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authCode.AccessToken);

            HttpResponseMessage httpResponse = await base.SendAsync(request, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {httpResponse.StatusCode}: {content}");
            }
            return httpResponse;
        }
        private async Task<AuthToken> GetAuthorizationToken(CancellationToken cancellationToken)
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("scope", "openid"),
            new("grant_type", "client_credentials")
        };

            using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);

            using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));

            authRequest.Content = authRequestContent;

            using HttpResponseMessage authorizationResponse = await base.SendAsync(authRequest, cancellationToken);

            authorizationResponse.EnsureSuccessStatusCode();

            return await authorizationResponse.Content.ReadFromJsonAsync<AuthToken>(cancellationToken);
        }
    }
    internal sealed class AuthToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }
    }
}
