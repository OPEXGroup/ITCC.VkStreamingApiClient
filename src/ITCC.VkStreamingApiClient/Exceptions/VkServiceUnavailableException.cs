// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.VkStreamingApiClient.Exceptions
{
    public class VkServiceUnavailableException : Exception
    {
        public VkServiceUnavailableException()
            : base("VK API service is unavailable")
        {
            
        }
    }
}
