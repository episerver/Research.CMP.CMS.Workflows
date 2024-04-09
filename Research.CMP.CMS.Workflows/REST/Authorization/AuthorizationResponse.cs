using System.Text.Json.Serialization;

namespace Research.CMP.CMS.Workflows.REST.Authorization
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}