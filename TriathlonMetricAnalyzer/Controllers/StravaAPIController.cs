using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class StravaAPIController : Controller
    {
        private readonly TokenStorageService userTokenStorage = new TokenStorageService();
        private readonly AthleteStorageService athleteStorage = new AthleteStorageService();
        private readonly SummaryActivitiesStorageService summaryActivitiesStorage = new SummaryActivitiesStorageService();
        private StravaAPIClient client;
        public StravaAPIController(TokenStorageService UserTokenStorage, AthleteStorageService AthleteStorage, SummaryActivitiesStorageService SummaryActivitiesStorage) 
        {
            userTokenStorage = UserTokenStorage;
            athleteStorage = AthleteStorage;
            summaryActivitiesStorage = SummaryActivitiesStorage;
            client = new StravaAPIClient(userTokenStorage);
        }

        public async Task<IActionResult> GetAuthenticatedAthlete()
        {
            athleteStorage.Athlete = await client.SendStravaGetAuthenticateAthleteRequest();
            if (athleteStorage.Athlete != null)
            {
                return PartialView("~/Views/Home/_AthletePartial.cshtml", athleteStorage.Athlete);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetAthleteActivities()
        {
            if (athleteStorage.Athlete == null)
            {
                return RedirectToAction("Index", "Home");
            }
            summaryActivitiesStorage.SummaryActivities = await client.SendStravaListAthleteActivitiesRequest();

            return RedirectToAction("Index", "Home");
        }
    }
}
