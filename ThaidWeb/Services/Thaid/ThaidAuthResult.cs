using System.Text.Json.Serialization;

namespace ThaidWeb.Services.Thaid
{
    public class ThaidAuthResult : ThaidError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
