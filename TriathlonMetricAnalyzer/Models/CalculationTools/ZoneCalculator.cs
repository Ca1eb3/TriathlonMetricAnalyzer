namespace TriathlonMetricAnalyzer.Models.CalculationTools
{
    public static class ZoneCalculator
    {
        public static int RunPaceZones(int Seconds, double PaceFactor)
        {
            double paceDouble = Seconds / 5 * PaceFactor;
            int pace = Convert.ToInt32(paceDouble);
            return pace;
        }

        public static List<int> BikeZones(int MaxAveragePower)
        {
            // T1 Zones
            int Zone1 = Convert.ToInt32(MaxAveragePower * .55);
            // T2 Zones
            int Zone2 = Convert.ToInt32(MaxAveragePower * .70);
            // T3 Zones
            int Zone3 = Convert.ToInt32(MaxAveragePower * .80);
            // T4 Zones
            int Zone4 = Convert.ToInt32(MaxAveragePower * .95);
            // T5 Zones
            int Zone5 = MaxAveragePower;
            // T6 Zones
            int Zone6 = Convert.ToInt32(MaxAveragePower * 1.30);
            // T7 Zones
            int Zone7 = Convert.ToInt32(MaxAveragePower * 1.75);
            List<int> BikeZones = new List<int> { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7 };
            return BikeZones;
        }

        public static int SwimPaceZones(int Seconds, float distance)
        {
            double distanceFactor = distance / 500;
            int time = Convert.ToInt32(Seconds * Math.Pow(distanceFactor, 1.11));
            int pace = Convert.ToInt32((time / distance) * 100);
            return pace;
        }
    }
}
