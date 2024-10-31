using Newtonsoft.Json;

namespace TriathlonMetricAnalyzer.Models.StravaAPIObjects
{
    public class WebhookEvent
    {
        private string objectType = string.Empty;
        private long objectId = 0;
        private string aspectType = string.Empty;
        private Dictionary<string, string> updates = new Dictionary<string, string>();
        private long ownerId = 0;
        private int subscriptionId = 0;
        private long eventTime = 0;

        [JsonProperty("object_type")]
        public string ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }


        [JsonProperty("object_id")]
        public long ObjectId
        {
            get { return objectId; }
            set { objectId = value; }
        }

        [JsonProperty("aspect_type")]
        public string AspectType
        {
            get { return aspectType; }
            set { aspectType = value; }
        }

        [JsonProperty("updates")]
        public Dictionary<string, string> Updates
        {
            get { return updates; }
            set { updates = value; }
        }

        [JsonProperty("owner_id")]
        public long OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        [JsonProperty("subscription_id")]
        public int SubscriptionId
        {
            get { return subscriptionId; }
            set { subscriptionId = value; }
        }

        [JsonProperty("event_time")]
        public long EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
        }

        // Method to retrieve a specific value by key
        public string GetValueByKey(string key)
        {
            // Check if the key exists and return the value; otherwise, return a default message
            if (updates.TryGetValue(key, out string value))
            {
                return value;
            }
            return "Key not found";
        }

        // Method to add or update a key-value pair
        public void Add(string key, string value)
        {
            updates[key] = value;
        }
    }
}
