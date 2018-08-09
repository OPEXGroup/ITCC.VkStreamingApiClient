// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Responses
{
    public class VkGetServerUrlResponse : VkResponseBase
    {
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
