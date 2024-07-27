using Newtonsoft.Json;
using System.Security.AccessControl;

namespace TriathlonMetricAnalyzer.Models.StravaAPIClient
{
    public class TokenResponse
    {
        private string accessToken = "N/A";
        private string refreshToken = "N/A";
        private int expiresIn = -1;
        private string tokenType = "N/A";
        private string scope = "N/A";

        [JsonProperty("access_token")]
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        [JsonProperty("refresh_token")]
        public string RefreshToken 
        {
            get { return refreshToken; }
            set { refreshToken = value; }
        }

        [JsonProperty("expires_in")]
        public int ExpiresIn 
        {
            get { return expiresIn; }
            set { expiresIn = value; }
        }

        [JsonProperty("token_type")]
        public string TokenType 
        {
            get { return tokenType; }
            set { tokenType = value; }
        }

        [JsonProperty("scope")]
        public string Scope 
        {
            get { return scope; }
            set { scope = value; }
        }
    }
}
