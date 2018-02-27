// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITCC.VkStreamingApiClient.Enums;
using ITCC.VkStreamingApiClient.Exceptions;
using ITCC.VkStreamingApiClient.Models.Enums;
using ITCC.VkStreamingApiClient.Models.Responses;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.API
{
    internal static class HttpRequestHelper
    {
        public static async Task<TResponse> GetRegularResponseAsync<TResponse>(HttpClient httpClient,
            string accessToken,
            RequestType requestType,
            IReadOnlyDictionary<string, string> parameters,
            CancellationToken cancellationToken) where TResponse : VkResponseBase, new()
        {
            DebugLogger.LogDebug($"Calling method {requestType}");
            var url = BuildRequestUrl(requestType, accessToken, parameters);
            DebugLogger.LogDebug($"Sending request to {url}");

            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken))
                {
                    httpResponseMessage.EnsureServiceIsAvailable();

                    var body = await httpResponseMessage.Content.ReadAsStringAsync();
                    DebugLogger.LogDebug($"Got response with code {httpResponseMessage.StatusCode}\n{body}");

                    var deserialized = JsonConvert.DeserializeObject<VkResponseHolder<TResponse>>(body);
                    DebugLogger.LogDebug($"Deserialized response of type {typeof(TResponse).Name}:\n{JsonConvert.SerializeObject(deserialized, Formatting.Indented)}");

                    deserialized.EnsureErrorIsNull();

                    return deserialized.Response;
                }
            }
        }

        

        private static string BuildRequestUrl(RequestType requestType, string accessToken, IReadOnlyDictionary<string, string> parameters)
        {
            var builder = new StringBuilder($"{VkApiBaseUrl}{UrlDictionary[requestType]}?v={ApiVersion}&{AccessTokenParamName}={accessToken}");
            if (parameters?.Any() != true)
                return builder.ToString();

            builder.Append("&");
            builder.Append(string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}")));
            return builder.ToString();
        }

        private static void EnsureServiceIsAvailable(this HttpResponseMessage httpResponseMessage)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    return;
                case HttpStatusCode.ServiceUnavailable:
                    throw new VkServiceUnavailableException();
                default:
                    var message = $"Unexpected status code: {(int)httpResponseMessage.StatusCode}";
                    DebugLogger.LogDebug(message);
                    throw new VkApiException(new VkError {ErrorCode = ErrorCode.InternalServerError});
            }
        }

        private const string ApiVersion = "5.73";
        private const string AccessTokenParamName = "access_token";
        private const string VkApiBaseUrl = "https://api.vk.com/method/";

        private static readonly Dictionary<RequestType, string> UrlDictionary = new Dictionary<RequestType, string>
        {
            [RequestType.GetServerUrl] = "streaming.getServerUrl",
            [RequestType.GetSettings]  = "streaming.getSettings"

        };
    }
}
