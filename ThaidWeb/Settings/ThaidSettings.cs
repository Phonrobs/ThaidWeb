namespace ThaidWeb.Settings;

public class ThaidSettings
{
    public string AuthEndpoint { get; set; }
    public string TokenEndpont { get; set; }
    public string RedirectUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

    public List<string> Scopes { get; set; }
}
