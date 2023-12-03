using IdentityModel.Client;

namespace UdemyMicroservices.Gateway.DelegateHandlers
{
    public class TokenExchangeDelegateHandler:DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accessToken;

        public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestToken = request.Headers.Authorization.Parameter;
            var newToken = await GetTokenAsync(requestToken);
            request.SetBearerToken(newToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetTokenAsync(string requestToken)
        {
            if (!string.IsNullOrEmpty(_accessToken)) {
                return _accessToken;
            }

            var discoveryDoc = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["IdentityServerURL"],
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discoveryDoc.IsError)
            {
                throw discoveryDoc.Exception;
            }

            TokenExchangeTokenRequest token = new()
            {
                Address = discoveryDoc.TokenEndpoint,
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                GrantType = "urn:ietf:params:oauth:grant-type:token-exchange",
                SubjectToken = requestToken,
                SubjectTokenType = "urn:ietf:params:oauth:token-type:access-token",
                Scope = "openid discount_fullpermission payment_fullpermission"
            };

            var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(token);

            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            _accessToken = tokenResponse.AccessToken;

            return _accessToken;
        }
    }
}
