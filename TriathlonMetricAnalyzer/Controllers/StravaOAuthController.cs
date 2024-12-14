using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class StravaOAuthController : Controller
    {
        private readonly string clientId;
        private readonly string clientSecret;

        public StravaOAuthController(IConfiguration configuration)
        {
            // Read values from appsettings.json directly
            clientId = configuration.GetValue<string>("Authentication:Client_Id");
            clientSecret = configuration.GetValue<string>("Authentication:Client_Secret");
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
                HttpContext.Session.SetString("UserToken", await ExchangeCodeForToken(code));
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(HttpContext.Session.GetString("UserToken"));
                HttpContext.Session.SetString("AthleteDetails", await StravaAPIClient.SendStravaGetAuthenticateAthleteRequest(tokenResponse.AccessToken));
                HttpContext.Session.SetString("SummaryActivities", await StravaAPIClient.SendStravaListAthleteActivitiesRequest(tokenResponse.AccessToken));
                return RedirectToAction("CalculateTLoad", "Metrics");
            }
            else
            {
                return Content("Error: Authorization code not found");
            }
        }

        private async Task<String> ExchangeCodeForToken(string code)
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
                    return responseString;
                }
                else
                {
                    throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
                }
            }
        }
    }
}
