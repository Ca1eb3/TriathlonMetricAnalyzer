using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Models.StravaAPIClient
{
    public static class StravaAPIClient
    {

        private static String baseUrl = $"https://www.strava.com/api/v3";

        public static async Task<DetailedAthlete> SendStravaGetAuthenticateAthleteRequest(string userToken)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/athlete");
            message.Headers.Add("Authorization", "Bearer " + userToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DetailedAthlete>(responseString);
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }

        public static async Task<List<SummaryActivity>> SendStravaListAthleteActivitiesRequest(string userToken)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/athlete/activities");
            message.Headers.Add("Authorization", "Bearer " + userToken);
            HttpClient client = new HttpClient();
            var response = client.Send(message);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                return JsonConvert.DeserializeObject<List<SummaryActivity>>(responseString, settings);
            }
            else
            {
                throw new Exception("Error exchanging code for token: " + response.ReasonPhrase);
            }
        }

        public static async Task<DetailedActivity> SendStravaDetailedActivityRequest(string userToken, long id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/activities/" + id);
            message.Headers.Add("Authorization", "Bearer " + userToken);
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

        public static async Task<StreamSet> SendStravaGetActivityStreamsRequest(string userToken, long id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, baseUrl + $"/activities/" + id + "/streams?keys=time,watts&key_by_type=true");
            message.Headers.Add("Authorization", "Bearer " + userToken);
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
