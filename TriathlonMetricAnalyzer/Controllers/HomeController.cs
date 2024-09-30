using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TriathlonMetricAnalyzer.Models;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SummaryActivitiesStorageService summaryActivitiesStorage = new SummaryActivitiesStorageService();
        private readonly AthleteStorageService athleteStorage = new AthleteStorageService();

        public HomeController(ILogger<HomeController> logger, AthleteStorageService AthleteStorage, SummaryActivitiesStorageService SummaryActivitiesStorage)
        {
            _logger = logger;
            athleteStorage = AthleteStorage;
            summaryActivitiesStorage = SummaryActivitiesStorage;
        }

        public IActionResult Index()
        {
            if (athleteStorage.Athlete != null)
            {
                ViewBag.Athlete = athleteStorage.Athlete.FirstName + " " + athleteStorage.Athlete.LastName;
            }
            return View();
        }

        public IActionResult Authorize()
        {
            return RedirectToAction("AuthorizeStrava", "StravaOAuth");
        }

        public IActionResult CalculateTLoad()
        {
            return RedirectToAction("CalculateTLoad", "Metrics");
        }

        public IActionResult Activities()
        {
            if (summaryActivitiesStorage.SummaryActivities != null)
            {
                return View(summaryActivitiesStorage.SummaryActivities);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Zones()
        {
            return RedirectToAction("CalculateZones", "StravaAPI");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}