// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using ITCC.VkStreamingApiClient.Models.Enums;

namespace ITCC.VkStreamingApiClient.Utils
{
    internal static class EnumHelper
    {
        #region StreamEventType

        private static readonly Dictionary<string, StreamEventType> StreamEventTypeDictionary
        = new Dictionary<string, StreamEventType>
            {
                ["post"]    = StreamEventType.Post,
                ["comment"] = StreamEventType.Comment,
                ["share"]   = StreamEventType.Share
            };

        public static StreamEventType StringToStreamEventType(string stringValue)
            => StringToEnum(stringValue, StreamEventTypeDictionary);

        #endregion

        #region StreamActionType

        private static readonly Dictionary<string, StreamActionType> StreamActionTypeDictionary
            = new Dictionary<string, StreamActionType>
            {
                ["new"]     = StreamActionType.New,
                ["update"]  = StreamActionType.Update,
                ["delete"]  = StreamActionType.Delete,
                ["restore"] = StreamActionType.Restore
            };

        public static StreamActionType StringToStreamActionType(string stringValue)
            => StringToEnum(stringValue, StreamActionTypeDictionary);

        #endregion

        #region AttachmentType

        private static readonly Dictionary<string, AttachmentType> AttachmentTypeDictionary
        = new Dictionary<string, AttachmentType>
            {
                ["photo"]        = AttachmentType.Photo,
                ["posted_photo"] = AttachmentType.PostedPhoto,
                ["video"]        = AttachmentType.Video,
                ["audio"]        = AttachmentType.Audio,
                ["doc"]          = AttachmentType.Document,
                ["graffiti"]     = AttachmentType.Graffiti,
                ["link"]         = AttachmentType.Link,
                ["note"]         = AttachmentType.Note,
                ["app"]          = AttachmentType.App,
                ["poll"]         = AttachmentType.Poll,
                ["page"]         = AttachmentType.Page,
                ["album"]        = AttachmentType.Album,
                ["photos_list"]  = AttachmentType.PhotosList,
                ["market"]       = AttachmentType.Market,
                ["market_album"] = AttachmentType.MarketAlbum,
                ["sticker"]      = AttachmentType.Sticker
            };

        public static AttachmentType StringToAttachmentTypeDictionary(string stringValue)
            => StringToEnum(stringValue, AttachmentTypeDictionary);

        #endregion

        #region Utils

        // ToDo: optimize with expression caching (avoid boxing)

        private static TEnum StringToEnum<TEnum>(string stringValue, IReadOnlyDictionary<string, TEnum> dictionary)
            where TEnum : struct, IConvertible
            => stringValue != null && dictionary.TryGetValue(stringValue, out var result) ? result : (TEnum)(object) 0;

        #endregion
    }
}
