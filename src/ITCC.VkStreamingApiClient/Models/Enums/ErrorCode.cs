// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace ITCC.VkStreamingApiClient.Models.Enums
{
    /// <summary>
    ///     General error code. See description <see href="https://vk.com/dev/errors">on website</see>
    /// </summary>
    public enum ErrorCode
    {
        NoError,
        /// <summary>
        ///     You should retry request later
        /// </summary>
        UnknownError = 1,
        /// <summary>
        ///     You should turn your app on <see href=" https://vk.com/editapp?id={API_ID}">admin page</see> or use test mode (test_mode = 1)
        /// </summary>
        ApplicationIsOff = 2,
        /// <summary>
        ///     For full method list see <see href="http://vk.com/dev/methods">method list</see>
        /// </summary>
        UnknownMethod = 3,
        /// <summary>
        ///     Bad request signature
        /// </summary>
        WrongSignature = 4,
        /// <summary>
        ///     Make sure you use right authorization scheme (see <see href="https://vk.com/dev/access_token">description</see>)
        /// </summary>
        UserAuthorizationFailed = 5,
        /// <summary>
        ///     Too many requests per second. See <see href="http://vk.com/dev/api_requests">limits</see>
        /// </summary>
        RequestRateExceeded = 6,

        PermissionDenied = 7,

        BadRequest = 8,

        TooManyRequestsOfTheSameType = 9,

        InternalServerError = 10,

        AppShouldBeTurnedOffInTestMode = 11,

        CaptchaRequired = 14,

        AccessDenied = 15,

        HttpsConnectionRequired = 16,

        UserValidationRequired = 17,

        PageIsBlockedOrDeleted = 18,

        ForbiddenForNonStandaloneApps = 20,

        StandaloneOrOpenApiAppRequired = 21,

        MethodIsNoLongerSupported = 23,

        UserConfirmationRequired = 24,

        GroupAccessKeyInvalid = 27,

        AppAccessKeyInvalid = 28,

        ParameterIsMissingOrInvalid = 100,

        WrongApiId = 101,

        WrongUserId = 113,

        WrongTimestamp = 150,

        AccessToAlbumDenied = 200,

        AccessToAudioDenied = 201,

        AccessToGroupDenied = 202,

        AlbumIsFull = 300,

        VoteTransferRequired = 500,

        AdsAccessDenied = 600,

        ErrorWorkingWithAds = 603
    }
}
