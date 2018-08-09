using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Entities
{
    public class GeoLocation
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("place", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Place Place { get; set; }
    }
}
