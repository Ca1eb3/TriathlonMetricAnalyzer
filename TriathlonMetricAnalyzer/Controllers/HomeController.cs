using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using TriathlonMetricAnalyzer.Models;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AthleteDetails") != null)
            {
                DetailedAthlete athlete = JsonConvert.DeserializeObject<DetailedAthlete>(HttpContext.Session.GetString("AthleteDetails"));
                ViewBag.Athlete = athlete.FirstName + " " + athlete.LastName;
            }
            return View();
        }

        public IActionResult Authorize()
        {
            return RedirectToAction("AuthorizeStrava", "StravaOAuth");
        }

        public IActionResult Activities()
        {
            if (HttpContext.Session.GetString("SummaryActivities") != null)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                List<SummaryActivity> activities = JsonConvert.DeserializeObject<List<SummaryActivity>>(HttpContext.Session.GetString("SummaryActivities"), settings);
                return View(activities);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Zones()
        {
            return RedirectToAction("CalculateZones", "Metrics");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}