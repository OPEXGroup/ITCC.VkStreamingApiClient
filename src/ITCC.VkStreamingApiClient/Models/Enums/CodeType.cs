// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace ITCC.VkStreamingApiClient.Models.Enums
{
    /// <summary>
    ///     All streaming API responses contain integer field "code"
    /// </summary>
    public enum CodeType
    {
        Undefined,

        Event = 100,

        Success = 200,

        ServiceMessage = 300,

        Error = 400
    }
}
