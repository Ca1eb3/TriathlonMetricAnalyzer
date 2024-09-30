using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TriathlonMetricAnalyzer.Models.CalculationTools;
using TriathlonMetricAnalyzer.Models.StorageServices;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class MetricsController : Controller
    {
        private readonly SummaryActivitiesStorageService summaryActivitiesStorage = new SummaryActivitiesStorageService();

        public MetricsController(SummaryActivitiesStorageService SummaryActivitiesStorage)
        {
            summaryActivitiesStorage = SummaryActivitiesStorage;
        }

        public IActionResult CalculateTLoad()
        {
            float TLoad = 0;
            foreach (SummaryActivity activity in summaryActivitiesStorage.SummaryActivities)
            {
                if (activity.StartDate > DateTime.Today.AddDays(-7))
                {
                    TLoad += (activity.Distance / activity.MovingTime) * 1; // the 1 is a place holder for a future calculated intensity factor
                }
            }
            return PartialView("~/Views/Home/_TLoadPartial.cshtml", TLoad);
        }

        public IActionResult CalculateZones() 
        {
            List<SummaryActivity> benchmarkActivities = summaryActivitiesStorage.SummaryActivities.Where(activity => activity.Name.Contains("BT")).ToList();
            SummaryActivity SwimBenchmark = null;
            SummaryActivity BikeBenchmark = null;
            SummaryActivity RunBenchmark = null;
            if (benchmarkActivities.Count > 0)
            {
                SwimBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Swim)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
                BikeBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Ride) || activity.SportType.Equals(SportType.VirtualRide)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
                RunBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Run)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
            }
            // Swim
            if (SwimBenchmark != null)
            {
                RedirectToAction("GetDetailedActivity", "StravaAPIController", SwimBenchmark.Id);
                //DetailedActivity activity = 
            }
            // Bike
            if (BikeBenchmark != null)
            {

            }

            // Run
            // The run zone computations are based on the assumption the run benchmark test was the complete duration of the recorded activity and had a total distance of 5km
            // Run Threshold Pace Zones
            if (RunBenchmark != null) 
            {
                // T1 Zones
                int Zone1 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, 1.35);
                // T2 Zones
                int Zone2 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, 1.25);
                // T3 Zones
                int Zone3 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, 1.2);
                // T4 Zones
                int Zone4 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, 1.15);
                // T5 Zones
                int Zone5 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, 1);
                // T6 Zones
                int Zone6 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, .85);
                // T7 Zones
                int Zone7 = ZoneCalculator.RunPaceZones(RunBenchmark.ElapsedTime, .65);
                List<int> RunZones = new List<int> { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7 };

                ViewBag.RunZones = RunZones;
            }

            return View("~/Views/Home/Zones.cshtml");
        }
    }
}
