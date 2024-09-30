using Newtonsoft.Json;
using System.Numerics;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class Lap
    {
        private long id = 0;
        private float distance = 0;
        private int elapsedTime = 0;
        private int split = 0;

        [JsonProperty("id")]
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty("distance")]
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [JsonProperty("elapsed_time")]
        public int ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        [JsonProperty("split")]
        public int Split
        {
            get { return split; }
            set { split = value; }
        }
    }
}
