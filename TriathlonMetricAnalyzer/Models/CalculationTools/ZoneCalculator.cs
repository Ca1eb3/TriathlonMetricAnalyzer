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

        public static int SwimPaceZones(int Seconds, float distance)
        {
            double distanceFactor = distance / 500;
            int time = Convert.ToInt32(Seconds * Math.Pow(distanceFactor, 1.11));
            int pace = Convert.ToInt32((time / distance) * 100);
            return pace;
        }
    }
}
