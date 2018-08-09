// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Models.Enums;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Responses
{
    public abstract class VkStreamingResponseBase : IErrorHolder
    {
        [JsonProperty("code")]
        public CodeType Code { get; set; }
        [JsonIgnore]
        public int CodeValue => (int)Code;

        [JsonProperty("error")]
        public VkError Error { get; set; }
    }
}
