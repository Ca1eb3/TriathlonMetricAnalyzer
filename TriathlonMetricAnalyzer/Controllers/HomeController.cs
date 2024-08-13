using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TriathlonMetricAnalyzer.Models;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}