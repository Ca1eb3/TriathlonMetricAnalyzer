using Microsoft.AspNetCore.Components.Forms;
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
        private readonly TokenStorageService userTokenStorage = new TokenStorageService();

        public MetricsController(TokenStorageService UserTokenStorage, SummaryActivitiesStorageService SummaryActivitiesStorage)
        {
            userTokenStorage = UserTokenStorage;
            summaryActivitiesStorage = SummaryActivitiesStorage;
        }

        public IActionResult CalculateTLoad()
        {
            List<float> TLoad = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (SummaryActivity activity in summaryActivitiesStorage.SummaryActivities)
            {
                DateTime startDateAtMidnight = activity.StartDate.Date;
                int daysDifference = (DateTime.Today - startDateAtMidnight).Days;
                if (daysDifference < 7)
                {
                    TLoad[7] += (activity.Distance / activity.MovingTime) * 1; // the 1 is a place holder for a future calculated intensity factor

                    switch (daysDifference)
                    {
                        case 0:
                            TLoad[0] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 1:
                            TLoad[1] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 2:
                            TLoad[2] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 3:
                            TLoad[3] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 4:
                            TLoad[4] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 5:
                            TLoad[5] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                        case 6:
                            TLoad[6] += (activity.Distance / activity.MovingTime) * 1;
                            continue;
                    }
                }
            }
            return PartialView("~/Views/Home/_TLoadPartial.cshtml", TLoad);
        }

        public async Task<IActionResult> CalculateZones()
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
                DetailedActivity activity = await StravaAPIClient.SendStravaDetailedActivityRequest(userTokenStorage.UserToken.AccessToken, SwimBenchmark.Id);
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
            // Bike
            if (BikeBenchmark != null)
            {
                StreamSet streamSet = await StravaAPIClient.SendStravaGetActivityStreamsRequest(userTokenStorage.UserToken.AccessToken, BikeBenchmark.Id);


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
