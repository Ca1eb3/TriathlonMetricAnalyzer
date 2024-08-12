using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Models.StravaAPIClient
{
    public class StravaAPIClient
    {

        private String url = $"https://www.strava.com/api/v3/athlete";
        private readonly TokenStorageService userTokenStorage;

        public StravaAPIClient(TokenStorageService UserTokenStorage)
        {
            userTokenStorage = UserTokenStorage;
        }


        public async Task<DetailedAthlete> SendStravaGetAuthenticateAthleteRequest()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Authorization", "Bearer " + userTokenStorage.UserToken.AccessToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                DetailedAthlete athlete = JsonConvert.DeserializeObject<DetailedAthlete>(responseString);
                return athlete;
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }
    }
}
