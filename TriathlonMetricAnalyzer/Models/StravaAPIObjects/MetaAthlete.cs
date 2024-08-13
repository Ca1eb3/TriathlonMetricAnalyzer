using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class MetaAthlete
    {
        private long id = 0;

        [JsonProperty("id")]
        public long Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
