// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Models.Entities;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Requests
{
    public class VkAddRuleRequest
    {
        [JsonProperty("rule")]
        public Rule Rule { get; set; }
    }
}
