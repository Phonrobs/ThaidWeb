using System.Text.Json.Serialization;

namespace ThaidWeb.Services.Thaid;

public class ThaidTokenResponse : ThaidError
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expire_in")]
    public int ExpireIn { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("id_token")]
    public ThaidIdToken IdToken { get; set; }
}
