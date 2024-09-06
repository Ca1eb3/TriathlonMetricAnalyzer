using Microsoft.AspNetCore.Mvc;
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
                    TLoad += activity.Distance;
                }
            }
            return PartialView("~/Views/Home/_TLoadPartial.cshtml", TLoad);
        }
    }
}
