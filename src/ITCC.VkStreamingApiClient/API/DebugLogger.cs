// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
#define WITH_ITCC_LOGGING

using System;
using System.Diagnostics;
using ITCC.Logging.Core;

namespace ITCC.VkStreamingApiClient.API
{
    internal static class DebugLogger
    {
        private const string LogContext = "VK STRM API";

        [Conditional("WITH_ITCC_LOGGING")]
        public static void LogDebug(string message)
            => Logger.LogEntry(LogContext, LogLevel.Debug, message);

        [Conditional("WITH_ITCC_LOGGING")]
        public static void LogDebug(Exception exception)
            => Logger.LogException(LogContext, LogLevel.Debug, exception);
    }
}
