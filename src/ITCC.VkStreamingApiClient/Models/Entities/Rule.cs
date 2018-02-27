// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Entities
{
    public class Rule
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
