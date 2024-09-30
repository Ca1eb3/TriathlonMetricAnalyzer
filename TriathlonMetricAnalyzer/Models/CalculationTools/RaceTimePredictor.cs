// Caleb Smith
// 09/09/2022
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriathlonMetricAnalyzer.Models.CalculationTools
{
    public static class RaceTimePredictor
    {
        // Methods
        // t2=t1* (d2/d1)^b
        // b = 1.06 typically for running
        // b = 1.15 current estimate for swimming

        public static int PredictTime2(int timeSeconds1, int distance1, int distance2)
        {
            double distanceFactor = Convert.ToDouble(distance2) / Convert.ToDouble(distance1);
            double TimeSeconds2 = timeSeconds1 * Math.Pow(distanceFactor, 1.06);
            int TimeSeconds2Rounded = Convert.ToInt32(TimeSeconds2);
            return TimeSeconds2Rounded;
        }
        public static int PredictTime2(int timeSeconds1, int distance1, int distance2, double bFactor)
        {
            double distanceFactor = Convert.ToDouble(distance2) / Convert.ToDouble(distance1);
            double TimeSeconds2 = timeSeconds1 * (Math.Pow(distanceFactor, bFactor));
            int TimeSeconds2Rounded = Convert.ToInt32(TimeSeconds2);
            return TimeSeconds2Rounded;
        }
    }
}
