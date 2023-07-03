using System.Text.Json.Serialization;

namespace ThaidWeb.Services.Thaid;

public class ThaidIdToken
{
    [JsonPropertyName("pid")]
    public string Pid { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("name_en")]
    public string NameEn { get; set; }

    [JsonPropertyName("birthdate")]
    public string BirthDate { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("given_name_en")]
    public string GivenNameEn { get; set; }

    [JsonPropertyName("middle_name_en")]
    public string MiddleNameEn { get; set; }

    [JsonPropertyName("family_name_en")]
    public string FamilyNameEn { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}
