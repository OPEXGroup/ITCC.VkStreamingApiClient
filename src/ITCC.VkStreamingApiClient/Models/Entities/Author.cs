// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Models.Enums;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Entities
{
    public class Author
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("author_url")]
        public string Url { get; set; }

        [JsonProperty("shared_post_author_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long SharedPostAuthorId { get; set; }

        [JsonProperty("shared_post_author_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SharedPostAuthorUrl { get; set; }

        [JsonProperty("platform")]
        public PlatformType Platform { get; set; }
        [JsonIgnore]
        public int PlatformValue => (int)Platform;
    }
}
