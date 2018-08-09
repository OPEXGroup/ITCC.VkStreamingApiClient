// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITCC.VkStreamingApiClient.Enums;
using ITCC.VkStreamingApiClient.Exceptions;
using ITCC.VkStreamingApiClient.Models.Entities;
using ITCC.VkStreamingApiClient.Models.Events;
using ITCC.VkStreamingApiClient.Models.Requests;
using ITCC.VkStreamingApiClient.Models.Responses;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.API
{
    public sealed class VkApiClient : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ClientWebSocket _clientWebSocket;
        private readonly string _accessToken;
        private volatile bool _isConnected;
        private volatile bool _stopRequested;
        private readonly ManualResetEventSlim _stoppedEvent = new ManualResetEventSlim();

        public string Endpoint { get; private set; }
        public string Key { get; private set; }

        public event EventHandler<VkStreamEvent> StreamEvent;
        public event EventHandler<VkStreamServiceMessage> ServiceMessage; 
        public event EventHandler<Exception> StreamException; 

        public VkApiClient(string accessToken)
        {
            _accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            _client = new HttpClient();
            _clientWebSocket = new ClientWebSocket();
        }

        #region stream rules processing

        public Task<Rule[]> GetRulesAsync() => GetRulesAsync(CancellationToken.None);

        public async Task<Rule[]> GetRulesAsync(CancellationToken cancellationToken)
        {
            var getRulesResponse = await GetStreamingResponseAsync<VkGetRulesResponse>(HttpMethod.Get, null, cancellationToken);

            return getRulesResponse.Rules ?? new Rule[0];
        }

        public Task AddRuleAsync(string tag, string value) => AddRuleAsync(tag, value, CancellationToken.None);

        public Task AddRuleAsync(string tag, string value, CancellationToken cancellationToken)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var addRuleRequest = new VkAddRuleRequest
            {
                Rule = new Rule
                {
                    Tag = tag,
                    Value = value
                }
            };

            return GetStreamingResponseAsync<VkAddRuleResponse>(HttpMethod.Post, addRuleRequest, cancellationToken);
        }

        public Task DeleteRuleAsync(string tag) => DeleteRuleAsync(tag, CancellationToken.None);

        public Task DeleteRuleAsync(string tag, CancellationToken cancellationToken)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var deleteRuleRequest = new VkDeleteRuleRequest
            {
                Tag = tag
            };

            return GetStreamingResponseAsync<VkDeleteRuleResponse>(HttpMethod.Delete, deleteRuleRequest, cancellationToken);
        }

        public Task DeleteAllRulesAsync() => DeleteAllRulesAsync(CancellationToken.None);

        public async Task DeleteAllRulesAsync(CancellationToken cancellationToken)
        {
            var rules = await GetRulesAsync(cancellationToken);

            foreach (var rule in rules)
            {
                await DeleteRuleAsync(rule.Tag, cancellationToken);
            }
        }

        #endregion

        #region streaming

        public Task StartListeningAsync() => StartListeningAsync(CancellationToken.None);

        public async Task StartListeningAsync(CancellationToken cancellationToken)
        {
            if (_isConnected)
                return;

            var url = $"wss://{Endpoint}/stream?key={Key}";
            await _clientWebSocket.ConnectAsync(new Uri(url), cancellationToken);
            DebugLogger.LogDebug($"Websocket connected to {url}");

            _isConnected = true;
            _stoppedEvent.Reset();
            RunWebSocketLoopAsync();
        }

        public bool Disconnect() => Disconnect(CancellationToken.None);

        public bool Disconnect(TimeSpan timeout)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(timeout))
            {
                return Disconnect(cancellationTokenSource.Token);
            }
        }

        public bool Disconnect(CancellationToken cancellationToken)
        {
            try
            {
                if (!_isConnected)
                    return true;

                _stopRequested = true;
                _stoppedEvent.Wait(cancellationToken);
                DebugLogger.LogDebug("Websocket disconnected");
                _isConnected = false;
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            catch (Exception e)
            {
                DebugLogger.LogDebug(e);
                throw new VkStreamException("Error disconnecting client websocket", e);
            }
        }

        #endregion

        #region raw method calls

        public Task<VkGetServerUrlResponse> GetServerUrlAsync() => GetServerUrlAsync(CancellationToken.None);

        public Task<VkGetServerUrlResponse> GetServerUrlAsync(CancellationToken cancellationToken)
        {
            return HttpRequestHelper.GetRegularResponseAsync<VkGetServerUrlResponse>(_client,
                _accessToken,
                RequestType.GetServerUrl,
                null,
                cancellationToken);
        }

        public Task<VkGetSettingsResponse> GetSettingsAsync() => GetSettingsAsync(CancellationToken.None);

        public Task<VkGetSettingsResponse> GetSettingsAsync(CancellationToken cancellationToken)
        {
            return HttpRequestHelper.GetRegularResponseAsync<VkGetSettingsResponse>(_client,
                _accessToken,
                RequestType.GetSettings,
                null,
                cancellationToken);
        }

        #endregion

        public Task ReadEndpointAndKeyAsync() => ReadEndpointAndKeyAsync(CancellationToken.None);

        public async Task ReadEndpointAndKeyAsync(CancellationToken cancellationToken)
        {
            try
            {
                var response = await GetServerUrlAsync(cancellationToken);

                Endpoint = response.Endpoint;
                Key = response.Key;
            }
            catch (Exception exception)
            {
                throw new VkStreamException("Error reading stream endpoint", exception);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
            _clientWebSocket.Dispose();
        }

        private async void RunWebSocketLoopAsync()
        {
            while (! _stopRequested)
            {
                try
                {
                    DebugLogger.LogDebug("Waiting for data...");
                    var receiveResult = await ReadUtf8StringAsync(_clientWebSocket);

                    var streamMessage = JsonConvert.DeserializeObject<VkStreamMessage>(receiveResult);
                    DebugLogger.LogDebug($"Object received:\n{JsonConvert.SerializeObject(streamMessage, Formatting.Indented)}");

                    HandleStreamMessage(streamMessage);
                }
                catch (Exception e)
                {
                    DebugLogger.LogDebug(e);
                    OnStreamException(e);
                }
            }

            _stoppedEvent.Set();
        }

        private void HandleStreamMessage(VkStreamMessage streamMessage)
        {
            if (streamMessage.Event != null)
                OnStreamEvent(streamMessage.Event);
            else if (streamMessage.ServiceMessage != null)
                OnServiceMessage(streamMessage.ServiceMessage);
        }

        private static async Task<string> ReadUtf8StringAsync(WebSocket webSocket)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);

            var totalBytes = 0;
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    DebugLogger.LogDebug($"{result.Count} bytes received");
                    totalBytes += result.Count;
                    if (result.EndOfMessage)
                        DebugLogger.LogDebug($"Whole message received ({totalBytes} bytes)");
                    // ReSharper disable once AssignNullToNotNullAttribute
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private async Task<TResponse> GetStreamingResponseAsync<TResponse>(HttpMethod httpMethod,
            object requestBody,
            CancellationToken cancellationToken)
            where TResponse : VkStreamingResponseBase
        {
            var url = $"https://{Endpoint}/rules?key={Key}";
            DebugLogger.LogDebug($"Sending request <{httpMethod.Method.ToUpperInvariant()} {url}>");

            using (var httpRequestMessage = new HttpRequestMessage(httpMethod, url))
            {
                if (requestBody != null)
                {
                    var serializedBody = JsonConvert.SerializeObject(requestBody);
                    httpRequestMessage.Content = new StringContent(serializedBody, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                using (var httpResponseMessage = await _client.SendAsync(httpRequestMessage, cancellationToken))
                {
                    var body = await httpResponseMessage.Content.ReadAsStringAsync();
                    DebugLogger.LogDebug($"Got response with code {httpResponseMessage.StatusCode}\n{body}");

                    var deserialized = JsonConvert.DeserializeObject<TResponse>(body);
                    DebugLogger.LogDebug($"Deserialized response of type {typeof(TResponse).Name}:\n{JsonConvert.SerializeObject(deserialized, Formatting.Indented)}");

                    deserialized.EnsureErrorIsNull();

                    return deserialized;
                }
            }
        }

        private void OnStreamEvent(VkStreamEvent e)
        {
            StreamEvent?.Invoke(this, e);
        }

        private void OnStreamException(Exception e)
        {
            StreamException?.Invoke(this, e);
        }

        private void OnServiceMessage(VkStreamServiceMessage e)
        {
            ServiceMessage?.Invoke(this, e);
        }
    }
}
