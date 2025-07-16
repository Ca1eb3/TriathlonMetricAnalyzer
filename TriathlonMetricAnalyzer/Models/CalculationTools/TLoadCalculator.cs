using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriathlonMetricAnalyzer.Models.StravaAPIObjects;

namespace TriathlonMetricAnalyzer.Models.CalculationTools
{
    public static class TLoadCalculator
    {
        public static List<float> CalculateTLoadLast7Days(List<SummaryActivity> SummaryActivities)
        {
            List<float> TLoad = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (SummaryActivity activity in SummaryActivities)
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
            return TLoad;
        }
    }
}
