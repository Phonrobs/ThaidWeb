using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using ThaidWeb.Exceptions;
using ThaidWeb.Helpers;
using ThaidWeb.Settings;

namespace ThaidWeb.Services.Thaid;

public class ThaidService : IThaidService
{
    private readonly ThaidSettings _settings;
    private readonly IDistributedCache _cache;
    private readonly IHttpClientFactory _httpClientFactory;

    public ThaidService(IOptions<ThaidSettings> options, IDistributedCache cache, IHttpClientFactory httpClientFactory)
    {
        _settings = options.Value;
        _cache = cache;
        _httpClientFactory = httpClientFactory;
    }

    public string GetAuthEndpoint()
    {
        var scopes = string.Join("%20", _settings.Scopes);
        var redirectUri = HttpUtility.UrlEncode(_settings.RedirectUri);
        var state = RandomString.GetRandomString(5);

        _cache.SetString(state, state, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(15)
        });

        return $"{_settings.AuthEndpoint}?response_type=code&client_id={_settings.ClientId}&redirect_uri={redirectUri}&scope={scopes}&state={state}";
    }

    public async Task<bool> IsValidStateAsync(string state)
    {
        var cachedState = await _cache.GetStringAsync(state);
        return cachedState == state;
    }

    public async Task<ThaidTokenResponse> GetTokenByAuthCodeAsync(string authCode)
    {
        //var redirectUri = HttpUtility.UrlEncode(_settings.RedirectUri);
        var redirectUri = _settings.RedirectUri;

        var formContent = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", authCode),
            new KeyValuePair<string, string>("redirect_uri", redirectUri)
        };

        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.ClientId}:{_settings.ClientSecret}"));

        var message = new HttpRequestMessage(
           HttpMethod.Post,
           _settings.TokenEndpoint)
        {
            Headers =
            {
                { HeaderNames.Authorization, $"Basic {authHeader}" }
            },
            Content = new FormUrlEncodedContent(formContent)
        };

        var client = _httpClientFactory.CreateClient();
        var response = await client.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            var errorString = await response.Content.ReadAsStringAsync();

            if(!string.IsNullOrEmpty(errorString))
            {
                var error = JsonSerializer.Deserialize<ThaidError>(errorString);
                throw new ThaidErrorException(error);
            }

            throw new Exception($"เกิดข้อผิดพลาดในการขอ Token: {response.StatusCode}");
        }

        var contentString = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrEmpty(contentString))
        {
            return JsonSerializer.Deserialize<ThaidTokenResponse>(contentString);
        }

        return null;
    }

    public async Task<string> SaveTokenInCacheAsync(ThaidTokenResponse token)
    {
        var tokenString = JsonSerializer.Serialize(token);

        var key =Guid.NewGuid().ToString();
        await _cache.SetStringAsync(key,tokenString);

        return key;
    }

    public async Task<ThaidTokenResponse> GetTokenFromCacheAsync(string key)
    {
        var tokenString = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(tokenString)) return null;

        return JsonSerializer.Deserialize<ThaidTokenResponse>(tokenString);
    }
}
