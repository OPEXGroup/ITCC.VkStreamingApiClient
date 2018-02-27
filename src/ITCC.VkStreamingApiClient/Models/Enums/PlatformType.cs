// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Diagnostics.CodeAnalysis;

namespace ITCC.VkStreamingApiClient.Models.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PlatformType
    {
        Undefined,
        MobileWeb = 1,
        IPad = 2,
        IPhone = 3,
        Android = 4,
        WindowsPhone = 5,
        Windows8 = 6,
        Web = 7,
        ExternalApp = 8
    }
}
