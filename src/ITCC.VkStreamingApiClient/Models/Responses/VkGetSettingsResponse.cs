// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Responses
{
    public class VkGetSettingsResponse : VkResponseBase
    {
        [JsonProperty("monthly_limit")]
        public string MontlyLimit { get; set; }
    }
}
