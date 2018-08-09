// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.VkStreamingApiClient.Exceptions;
using ITCC.VkStreamingApiClient.Models.Responses;

namespace ITCC.VkStreamingApiClient.API
{
    internal static class ErrorHelper
    {
        public static void EnsureErrorIsNull(this IErrorHolder errorHolder)
        {
            if (errorHolder?.Error == null)
                return;

            throw new VkApiException(errorHolder.Error);
        }
    }
}
