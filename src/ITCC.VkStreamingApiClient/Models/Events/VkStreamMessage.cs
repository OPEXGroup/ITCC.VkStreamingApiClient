// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Models.Enums;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Events
{
    public class VkStreamMessage
    {
        [JsonProperty("code")]
        public CodeType Code { get; set; }

        [JsonProperty("service_message")]
        public VkStreamServiceMessage ServiceMessage { get; set; }

        [JsonProperty("event")]
        public VkStreamEvent Event { get; set; }
    }
}
