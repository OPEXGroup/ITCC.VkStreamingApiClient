// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.VkStreamingApiClient.Utils
{
    internal static class DateHelper
    {
        public static DateTime? ToVkDateTime(this long timestamp)
            => timestamp == 0 ? Unspecified : EpochTime.AddSeconds(timestamp);

        private static readonly DateTime? Unspecified = null;
        private static readonly DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
