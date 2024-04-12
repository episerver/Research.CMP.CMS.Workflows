using System.Text.Json.Serialization;

namespace Research.CMP.CMS.Workflows.REST.Authorization
{
    public class AuthorizationRequest
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "client_credentials";

        [JsonPropertyName("scope")]
        public string Scope { get; set; } = "scope";

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
    }
}