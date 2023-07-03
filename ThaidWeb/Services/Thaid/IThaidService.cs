namespace ThaidWeb.Services.Thaid;

public interface IThaidService
{
    string GetAuthEndpoint();
    Task<ThaidTokenResponse> GetTokenByAuthCodeAsync(string authCode);
    Task<ThaidTokenResponse> GetTokenFromCacheAsync(string key);
    Task<bool> IsValidStateAsync(string state);
    Task<string> SaveTokenInCacheAsync(ThaidTokenResponse token);
}
