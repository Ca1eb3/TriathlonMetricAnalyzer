using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Controllers
{
    [Route("Webhook/Webhook")]
    public class WebhookController : Controller
    {
        // A constant token to use for verification
        private readonly string VERIFY_TOKEN;

        public WebhookController(IConfiguration configuration)
        {
            // Read values from appsettings.json directly
            VERIFY_TOKEN = configuration.GetValue<string>("Authentication:Verified_Token");
        }

        // POST Webhook/Webhook
        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            // Process the webhook data
            // Read the post data from the request body
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                string postData = await reader.ReadToEndAsync();
                // Process the post data as needed
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                WebhookEvent webhookEvent = JsonConvert.DeserializeObject<WebhookEvent>(postData, settings);
                Console.WriteLine("Test");
            }

            // TODO: Add logic to handle the webhook
            // handle authorized status change

            // Return a 200 OK response to acknowledge receipt
            return Ok();
        }

        // GET Webhook/Webhook
        [HttpGet]
        public IActionResult Webhook(
            [FromQuery(Name = "hub.mode")] string hub_mode,
            [FromQuery(Name = "hub.verify_token")] string hub_verify_token,
            [FromQuery(Name = "hub.challenge")] string hub_challenge)
        {
            // Check if the mode and token are present in the query parameters
            if (hub_mode == "subscribe" && hub_verify_token == VERIFY_TOKEN)
            {
                // Log verification and return the challenge to confirm the callback URL
                Console.WriteLine("WEBHOOK_VERIFIED");
                var response = new Dictionary<string, string>
                {
                    { "hub.challenge", hub_challenge } // Add the challenge with the exact key
                };
                return Ok(response);
            }
            else
            {
                // Respond with 403 Forbidden if the token does not match
                return Forbid();
            }
        }
    }
}
