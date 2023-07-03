using System.Text.Json.Serialization;

namespace ThaidWeb.Services.Thaid;

public class ThaidTokenRequest
{
    //[JsonPropertyName("authorization")]
    //public string Authorization { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; set; }
}
