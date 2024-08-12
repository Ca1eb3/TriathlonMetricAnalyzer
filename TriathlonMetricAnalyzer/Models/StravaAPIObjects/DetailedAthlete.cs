using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class DetailedAthlete
    {
        private long id = 0;
        private int resourceState = 0;
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string profileMedium = string.Empty;
        private string profile = string.Empty;
        private string city = string.Empty;
        private string state = string.Empty;
        private string country = string.Empty;
        private string sex = string.Empty;
        private bool summit = false;
        private DateTime createdAt = DateTime.MinValue;
        private DateTime updatedAt = DateTime.MinValue;
        private int followerCount = 0;
        private int friendCount = 0;
        private string measurementPreference = string.Empty;
        private int ftp = 0;
        private float weight = 0;

        [JsonProperty("id")]
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty("resource_state")]
        public int ResourceState
        {
            get { return resourceState; }
            set { resourceState = value; }
        }

        [JsonProperty("firstname")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [JsonProperty("lastname")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [JsonProperty("profile_medium")]
        public string ProfileMedium
        {
            get { return profileMedium; }
            set { profileMedium = value; }
        }

        [JsonProperty("profile")]
        public string Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        [JsonProperty("city")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [JsonProperty("state")]
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        [JsonProperty("country")]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        [JsonProperty("sex")]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        [JsonProperty("summit")]
        public bool Summit
        {
            get { return summit; }
            set { summit = value; }
        }

        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }

        [JsonProperty("follower_count")]
        public int FollowerCount
        {
            get { return followerCount; }
            set { followerCount = value; }
        }

        [JsonProperty("friend_count")]
        public int FriendCount
        {
            get { return friendCount; }
            set { friendCount = value; }
        }

        [JsonProperty("measurement_preference")]
        public string MeasurementPreference
        {
            get { return measurementPreference; }
            set { measurementPreference = value; }
        }

        [JsonProperty("ftp")]
        public int Ftp
        {
            get { return ftp; }
            set { ftp = value; }
        }

        [JsonProperty("weight")]
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }
}
