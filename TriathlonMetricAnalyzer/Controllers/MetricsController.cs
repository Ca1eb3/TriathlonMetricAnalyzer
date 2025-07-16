using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TriathlonMetricAnalyzer.Models.CalculationTools;
using TriathlonMetricAnalyzer.Models.StravaAPIClient;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Controllers
{
    public class MetricsController : Controller
    {
        public async Task<IActionResult> CalculateZones()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            if (HttpContext.Session.GetString("SummaryActivities") == null)
            {
                return NotFound();
            }

            List<SummaryActivity> activities = JsonConvert.DeserializeObject<List<SummaryActivity>>(HttpContext.Session.GetString("SummaryActivities"), settings);
            List<SummaryActivity> benchmarkActivities = activities.Where(activity => activity.Name.Contains("BT")).ToList();
            SummaryActivity SwimBenchmark = null;
            SummaryActivity BikeBenchmark = null;
            SummaryActivity RunBenchmark = null;
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(HttpContext.Session.GetString("UserToken"));
            if (benchmarkActivities.Count > 0)
            {
                SwimBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Swim)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
                BikeBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Ride) || activity.SportType.Equals(SportType.VirtualRide)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
                RunBenchmark = benchmarkActivities.Where(activity => activity.SportType.Equals(SportType.Run)).OrderByDescending(obj => obj.StartDate).FirstOrDefault();
            }
            // Swim
            if (SwimBenchmark != null)
            {
                DetailedActivity activity = await StravaAPIClient.SendStravaDetailedActivityRequest(tokenResponse.AccessToken, SwimBenchmark.Id);
                Lap lap = activity.Laps.OrderBy(lap => Math.Abs(lap.Distance - 500)).FirstOrDefault();

                // T1 Zones
                int Zone1 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 5000);
                // T2 Zones
                int Zone2 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 3000);
                // T3 Zones
                int Zone3 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 1500);
                // T4 Zones
                int Zone4 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 1000);
                // T5 Zones
                int Zone5 = lap.ElapsedTime / 5;
                // T6 Zones
                int Zone6 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 200);
                // T7 Zones
                int Zone7 = ZoneCalculator.SwimPaceZones(lap.ElapsedTime, 50);
                List<int> SwimZones = new List<int> { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7 };

                ViewBag.SwimZones = SwimZones;
            }
            else
            {
                List<int> SwimZones = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

                ViewBag.SwimZones = SwimZones;
            }

            // Bike
            if (BikeBenchmark != null)
            {
                StreamSet streamSet = await StravaAPIClient.SendStravaGetActivityStreamsRequest(tokenResponse.AccessToken, BikeBenchmark.Id);


                int i = 1200;
                int maxAveragePower = 0;
                while (i < streamSet.Power.Data.Count)
                {
                    float sum = 0;
                    for (int j = i - 1200; j <= i; j++)
                    {
                        sum += streamSet.Power.Data[j];
                    }
                    int averagePower = Convert.ToInt32(sum / 1200);
                    if (averagePower > maxAveragePower)
                    {
                        maxAveragePower = averagePower;
                    }
                    i++;
                }

                // T1 Zones
                int Zone1 = Convert.ToInt32(maxAveragePower * .55);
                // T2 Zones
                int Zone2 = Convert.ToInt32(maxAveragePower * .70);
                // T3 Zones
                int Zone3 = Convert.ToInt32(maxAveragePower * .80);
                // T4 Zones
                int Zone4 = Convert.ToInt32(maxAveragePower * .95);
                // T5 Zones
                int Zone5 = maxAveragePower;
                // T6 Zones
                int Zone6 = Convert.ToInt32(maxAveragePower * 1.30);
                // T7 Zones
                int Zone7 = Convert.ToInt32(maxAveragePower * 1.75);
                List<int> BikeZones = new List<int> { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7 };

                ViewBag.BikeZones = BikeZones;
            }
            else
            {
                List<int> BikeZones = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

                ViewBag.BikeZones = BikeZones;
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
            else
            {
                List<int> RunZones = new List<int> { 0, 0, 0, 0, 0, 0, 0 };

                ViewBag.RunZones = RunZones;
            }

            return View("~/Views/Home/Zones.cshtml");
        }

        public async Task<IActionResult> Estimator()
        {
            return View("~/Views/Home/Estimator.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> EstimateRace(double swimDistance = 1.5, int swimMinutes = 2, int swimSeconds = 0, int t1Minutes = 1, int t1Seconds = 0, double bikeDistance = 40, double bikeSpeed = 35, int t2Minutes = 0, int t2Seconds = 45, double runDistance = 10, int runMinutes = 4, int runSeconds = 0)
        {
            // Pass Estimator values through to view
            ViewBag.SwimDistance = swimDistance;
            ViewBag.SwimMinutes = swimMinutes;
            ViewBag.SwimSeconds = swimSeconds;
            ViewBag.T1Minutes = t1Minutes;
            ViewBag.T1Seconds = t1Seconds;
            ViewBag.BikeDistance = bikeDistance;
            ViewBag.BikeSpeed = bikeSpeed;
            ViewBag.T2Minutes = t2Minutes;
            ViewBag.T2Seconds = t2Seconds;
            ViewBag.RunDistance = runDistance;
            ViewBag.RunMinutes = runMinutes;
            ViewBag.RunSeconds = runSeconds;

            // Compute Swim Time Seconds
            int swimPace = swimMinutes * 60 + swimSeconds;
            ViewBag.SwimTime = Convert.ToInt32(((swimDistance * 1000) / 100) * swimPace);

            // Compute Bike Time Seconds
            ViewBag.BikeTime = Convert.ToInt32((bikeDistance / bikeSpeed) * 3600);

            // Compute Run Time
            int runPace = runMinutes * 60 + runSeconds;
            ViewBag.RunTime = Convert.ToInt32(runDistance * runPace);

            // Compute Total Time
            ViewBag.Time = Convert.ToInt32(ViewBag.SwimTime + (t1Minutes * 60) + t1Seconds + ViewBag.BikeTime + (t2Minutes * 60) + t2Seconds + ViewBag.RunTime);

            return View("~/Views/Home/Estimator.cshtml");
        }
    }
}
