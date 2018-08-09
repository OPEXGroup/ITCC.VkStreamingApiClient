// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.VkStreamingApiClient.Models.Responses;

namespace ITCC.VkStreamingApiClient.Exceptions
{
    public class VkApiException : Exception
    {
        public VkApiException(VkError vkError)
        {
            VkError = vkError;
        }

        public VkError VkError { get; }
    }
}
