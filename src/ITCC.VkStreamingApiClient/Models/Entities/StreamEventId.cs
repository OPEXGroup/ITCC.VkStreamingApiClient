// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Entities
{
    public class StreamEventId
    {
        [JsonProperty("post_owner_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long PostOwnerId { get; set; }

        [JsonProperty("post_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long PostId { get; set; }

        [JsonProperty("comment_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long CommentId { get; set; }

        [JsonProperty("shared_post_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long SharedPostId { get; set; }
    }
}
