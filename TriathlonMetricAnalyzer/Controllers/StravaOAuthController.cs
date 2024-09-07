using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class StravaOAuthController : Controller
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly TokenStorageService userTokenStorage = new TokenStorageService();

        public StravaOAuthController(IConfiguration configuration, TokenStorageService UserTokenStorage)
        {
            // Read values from appsettings.json directly
            clientId = configuration.GetValue<string>("Authentication:Client_Id");
            clientSecret = configuration.GetValue<string>("Authentication:Client_Secret");
            userTokenStorage = UserTokenStorage;
        }

        public ActionResult AuthorizeStrava()
        {
            string redirectUri = Url.Action("StravaCallback", "StravaOAuth", null, Request.Scheme);
            string authorizationUrl = $"https://www.strava.com/oauth/authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope=read_all,activity:read_all,activity:write&state=mystate";
            return Redirect(authorizationUrl);
        }

        public async Task<ActionResult> StravaCallback(string code, string state)
        {
            if (!string.IsNullOrEmpty(code))
            {
                // Exchange the authorization code for an access token
                userTokenStorage.UserToken = await ExchangeCodeForToken(code);
                return RedirectToAction("GetAuthenticatedAthlete", "StravaAPI");
            }
            else
            {
                return Content("Error: Authorization code not found");
            }
        }

        private async Task<TokenResponse> ExchangeCodeForToken(string code)
        {
            string redirectUri = Url.Action("StravaCallback", "StravaOAuth", null, Request.Scheme);

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", redirectUri }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://www.strava.com/oauth/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                    return tokenResponse;
                }
                else
                {
                    throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
                }
            }
        }
    }
}
