// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Requests
{
    public class VkDeleteRuleRequest
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }
}
