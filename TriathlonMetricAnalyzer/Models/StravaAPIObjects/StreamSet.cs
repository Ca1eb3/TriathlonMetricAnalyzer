using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class StreamSet
    {
        private TimeStream time = new TimeStream();
        private DistanceStream distance = new DistanceStream();
        private PowerStream power = new PowerStream();


        [JsonProperty("time")]
        public TimeStream Time
        {
            get { return time; }
            set { time = value; }
        }

        [JsonProperty("distance")]
        public DistanceStream Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [JsonProperty("watts")]
        public PowerStream Power
        {
            get { return power; }
            set { power = value; }
        }
    }
}
