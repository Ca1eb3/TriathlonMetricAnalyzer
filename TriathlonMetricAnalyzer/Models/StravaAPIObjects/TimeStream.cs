using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class TimeStream
    {
        private int originalSize = 0;
        private string resolution = string.Empty;
        private string seriesType = string.Empty;
        private List<float> data = new List<float>();


        [JsonProperty("original_size")]
        public int OriginalSize
        {
            get { return originalSize; }
            set { originalSize = value; }
        }

        [JsonProperty("resolution")]
        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        [JsonProperty("series_type")]
        public string SeriesType
        {
            get { return seriesType; }
            set { seriesType = value; }
        }

        [JsonProperty("data")]
        public List<float> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
