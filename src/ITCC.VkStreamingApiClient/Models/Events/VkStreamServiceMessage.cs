// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Events
{
    public class VkStreamServiceMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        // ToDo: enum
        [JsonProperty("service_code")]
        public int ServiceCode { get; set; }
    }
}
