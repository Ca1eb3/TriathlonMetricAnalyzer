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

        public HomeController(ILogger<HomeController> logger, SummaryActivitiesStorageService SummaryActivitiesStorage)
        {
            _logger = logger;
            summaryActivitiesStorage = SummaryActivitiesStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authorize()
        {
            return RedirectToAction("AuthorizeStrava", "StravaOAuth");
        }

        public IActionResult GetAthlete()
        {
            return RedirectToAction("GetAuthenticatedAthlete", "StravaAPI");
        }

        public IActionResult GetAthleteActivities()
        {
            return RedirectToAction("GetAthleteActivities", "StravaAPI");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}