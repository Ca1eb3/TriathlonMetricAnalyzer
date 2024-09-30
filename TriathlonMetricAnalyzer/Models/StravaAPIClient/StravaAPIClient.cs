using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Models.StravaAPIClient
{
    public class StravaAPIClient
    {

        private String baseUrl = $"https://www.strava.com/api/v3";
        private readonly TokenStorageService userTokenStorage;

        public StravaAPIClient(TokenStorageService UserTokenStorage)
        {
            userTokenStorage = UserTokenStorage;
        }


        public async Task<DetailedAthlete> SendStravaGetAuthenticateAthleteRequest()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/athlete");
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

        public async Task<List<SummaryActivity>> SendStravaListAthleteActivitiesRequest()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/athlete/activities");
            message.Headers.Add("Authorization", "Bearer " + userTokenStorage.UserToken.AccessToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                List<SummaryActivity> summaryActivities = JsonConvert.DeserializeObject<List<SummaryActivity>>(responseString, settings);
                return summaryActivities;
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }

        public async Task<DetailedActivity> SendStravaDetailedActivityRequest(long id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/activities/" + id);
            message.Headers.Add("Authorization", "Bearer " + userTokenStorage.UserToken.AccessToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                DetailedActivity activity = JsonConvert.DeserializeObject<DetailedActivity>(responseString, settings);
                return activity;
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }

        public async Task<StreamSet> SendStravaGetActivityStreamsRequest(long id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/activities/" + id + "/streams?keys=time,watts&key_by_type=true");
            message.Headers.Add("Authorization", "Bearer " + userTokenStorage.UserToken.AccessToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                StreamSet stream = JsonConvert.DeserializeObject<StreamSet>(responseString, settings);
                return stream;
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }
    }
}
