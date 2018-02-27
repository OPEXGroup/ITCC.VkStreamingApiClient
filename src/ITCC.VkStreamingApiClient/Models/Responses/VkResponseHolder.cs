// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Responses
{
    public class VkResponseHolder<TResponse> : IErrorHolder
        where TResponse : VkResponseBase, new()
    {
        [JsonProperty("response")]
        public TResponse Response { get; set; }

        [JsonProperty("error")]
        public VkError Error { get; set; }
    }
}
