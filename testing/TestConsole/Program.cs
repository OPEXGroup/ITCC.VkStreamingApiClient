// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCC.Logging.Core;
using ITCC.Logging.Windows.Loggers;
using ITCC.VkStreamingApiClient.API;
using ITCC.VkStreamingApiClient.Models.Entities;
using Newtonsoft.Json;
using VkNet;
using VkNet.Enums.Filters;

namespace TestConsole
{
    internal static class Program
    {
        private const string ClientSecretsFilename = "client_secrets.json";

        private static void Main() => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Logger.Level = LogLevel.Trace;
            Logger.RegisterReceiver(new ColouredConsoleLogger());

            var authData = ReadAuthData();

            var token = await GetAccessTokenAsync(authData);
            if (token == null)
                return;

            Console.WriteLine($"Got access token: {token.Substring(0, 12)}...");

            try
            {
                using (var client = new VkApiClient(token))
                {
                    await client.ReadEndpointAndKeyAsync();

                    await client.GetSettingsAsync();

                    var ruleTag = "cat";
                    var ruleValue = "котэ";

                    var rules = await client.GetRulesAsync();
                    foreach (var rule in rules)
                    {
                        PrintRule(rule);
                    }

                    if (rules.All(r => r.Tag != ruleTag))
                        await client.AddRuleAsync(ruleTag, ruleValue);

                    rules = await client.GetRulesAsync();
                    foreach (var rule in rules)
                    {
                        PrintRule(rule);
                    }

                    await client.StartListeningAsync();
                    Console.ReadLine();
                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                Logger.LogException("ERROR", LogLevel.Warning, e);
            }
        }

        private static void PrintRule(Rule rule)
        {
            Logger.LogEntry("RULE", LogLevel.Debug, $"{rule.Tag}: {rule.Value}");
        }

        private static async Task<string> GetAccessTokenAsync(AuthData authData)
        {
            try
            {
                var api = new VkApi();
                await api.AuthorizeAsync(new ApiAuthParams
                {
                    Login = authData.Username,
                    Password = NullIfEmpty(authData.Password),
                    ApplicationId = authData.ApplicationId,
                    Settings = Settings.All,
                    TwoFactorAuthorization = PromptForSecondAuthFactor,
                    AccessToken = NullIfEmpty(authData.AccessToken)
                });

                return api.Token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static string PromptForSecondAuthFactor()
        {
            Console.Write("Please enter code from app/sms: ");
            return Console.ReadLine();
        }

        private static AuthData ReadAuthData()
        {
            if (! File.Exists(ClientSecretsFilename))
                return AuthData.ReadFromConsole();

            try
            {
                var rawData = File.ReadAllText(ClientSecretsFilename, Encoding.UTF8);
                return JsonConvert.DeserializeObject<AuthData>(rawData);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return AuthData.ReadFromConsole();
            }
        }

        private static string NullIfEmpty(string s) => string.IsNullOrEmpty(s) ? null : s;
    }
}
