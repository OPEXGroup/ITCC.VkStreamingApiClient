// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using Newtonsoft.Json;

namespace TestConsole
{
    internal class AuthData
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("application_id")]
        public ulong ApplicationId { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public static AuthData ReadFromConsole()
        {
            var result = new AuthData();
            Console.Write("username: ");
            result.Username = Console.ReadLine();
            Console.Write("password: ");
            result.Password = Console.ReadLine();
            while (true)
            {
                Console.Write("application_id: ");
                var userInput = Console.ReadLine();
                if (ulong.TryParse(userInput, out var applicationId))
                {
                    result.ApplicationId = applicationId;
                    return result;
                }

                Console.WriteLine("Bad value!");
            }
        }
    }
}
