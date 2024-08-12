using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class StravaAPIController : Controller
    {
        private readonly TokenStorageService userTokenStorage = new TokenStorageService();
        private DetailedAthlete athlete = new DetailedAthlete();
        public StravaAPIController(TokenStorageService UserTokenStorage) 
        {
            userTokenStorage = UserTokenStorage;
        }

        public async Task<IActionResult> GetAuthenticatedAthlete()
        {
            StravaAPIClient client = new StravaAPIClient(userTokenStorage);
            athlete = await client.SendStravaGetAuthenticateAthleteRequest();
            if (athlete != null)
            {
                return PartialView("~/Views/Home/_AthletePartial.cshtml", athlete);
            }



            return RedirectToAction("Index", "Home");
        }
    }
}
