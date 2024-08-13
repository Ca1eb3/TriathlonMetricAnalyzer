using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class SummaryActivity
    {
        private long id = 0;
        private string externalId = string.Empty;
        private long uploadId = 0;
        private MetaAthlete athlete = new MetaAthlete();
        private string name = string.Empty;
        private float distance = 0;
        private int movingTime = 0;
        private int elapsedTime = 0;
        private float totalElevationGain = 0;
        private float elevationHigh = 0;
        private float elevationLow = 0;
        private SportType sportType = SportType.None;
        private DateTime startDate = DateTime.MinValue;
        private DateTime startDateLocal = DateTime.MinValue;
        private string timezone = string.Empty;
        private bool trainer = false;
        private int workoutType = 0;
        private float averageSpeed = 0;
        private float maxSpeed = 0;
        private float kilojoules = 0;
        private float averageWatts = 0;
        private bool deviceWatts = false;
        private int maxWatts = 0;
        private int weightedAverageWatts = 0;

        [JsonProperty("id")]
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty("external_id")]
        public string ExternalId
        {
            get { return externalId; }
            set { externalId = value; }
        }

        [JsonProperty("upload_id")]
        public long UploadId
        {
            get { return uploadId; }
            set { uploadId = value; }
        }

        [JsonProperty("athlete")]
        public MetaAthlete Athlete
        {
            get { return athlete; }
            set { athlete = value; }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty("distance")]
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [JsonProperty("moving_time")]
        public int MovingTime
        {
            get { return movingTime; }
            set { movingTime = value; }
        }

        [JsonProperty("elapsed_time")]
        public int ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        [JsonProperty("total_elevation_gain")]
        public float TotalElevationGain
        {
            get { return totalElevationGain; }
            set { totalElevationGain = value; }
        }

        [JsonProperty("elev_high")]
        public float ElevationHigh
        {
            get { return elevationHigh; }
            set { elevationHigh = value; }
        }

        [JsonProperty("elev_low")]
        public float ElevationLow
        {
            get { return elevationLow; }
            set { elevationLow = value; }
        }

        [JsonProperty("sport_type")]
        public SportType SportType
        {
            get { return sportType; }
            set { sportType = value; }
        }

        [JsonProperty("start_date")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [JsonProperty("start_date_local")]
        public DateTime StartDateLocal
        {
            get { return startDateLocal; }
            set { startDateLocal = value; }
        }

        [JsonProperty("timezone")]
        public string Timezone
        {
            get { return timezone; }
            set { timezone = value; }
        }

        [JsonProperty("trainer")]
        public bool Trainer
        {
            get { return trainer; }
            set { trainer = value; }
        }

        [JsonProperty("workout_type")]
        public int WorkoutType
        {
            get { return workoutType; }
            set { workoutType = value; }
        }

        [JsonProperty("average_speed")]
        public float AverageSpeed
        {
            get { return averageSpeed; }
            set { averageSpeed = value; }
        }

        [JsonProperty("max_speed")]
        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        [JsonProperty("kilojoules")]
        public float Kilojoules
        {
            get { return kilojoules; }
            set { kilojoules = value; }
        }

        [JsonProperty("average_watts")]
        public float AverageWatts
        {
            get { return averageWatts; }
            set { averageWatts = value; }
        }

        [JsonProperty("device_watts")]
        public bool DeviceWatts
        {
            get { return deviceWatts; }
            set { deviceWatts = value; }
        }

        [JsonProperty("max_watts")]
        public int MaxWatts
        {
            get { return maxWatts; }
            set { maxWatts = value; }
        }

        [JsonProperty("weighted_average_watts")]
        public int WeightedAverageWatts
        {
            get { return weightedAverageWatts; }
            set { weightedAverageWatts = value; }
        }
    }
}
