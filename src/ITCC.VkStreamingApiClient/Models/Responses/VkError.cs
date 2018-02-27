// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Models.Enums;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Responses
{
    public class VkError
    {
        [JsonProperty("error_code")]
        public ErrorCode ErrorCode { get; set; }
        [JsonIgnore]
        public int ErrorCodeValue => (int)ErrorCode;

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
