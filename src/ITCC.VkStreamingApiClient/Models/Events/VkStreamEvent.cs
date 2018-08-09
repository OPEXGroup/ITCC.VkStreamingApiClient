// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.VkStreamingApiClient.Models.Entities;
using ITCC.VkStreamingApiClient.Models.Enums;
using ITCC.VkStreamingApiClient.Utils;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Events
{
    public class VkStreamEvent
    {
        [JsonProperty("event_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string EventTypeValue { get; set; }
        [JsonIgnore]
        public StreamEventType EventType => EnumHelper.StringToStreamEventType(EventTypeValue);

        [JsonProperty("event_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public StreamEventId EventId { get; set; }

        [JsonProperty("event_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string EventUrl { get; set; }

        [JsonProperty("text", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("action", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ActionValue { get; set; }
        [JsonIgnore]
        public StreamActionType Action => EnumHelper.StringToStreamActionType(ActionValue);

        [JsonProperty("action_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ActionTimeValue { get; set; }
        [JsonIgnore]
        public DateTime? ActionTime => ActionTimeValue.ToVkDateTime();

        [JsonProperty("creation_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long CreationTimeValue { get; set; }
        [JsonIgnore]
        public DateTime? CreationTime => CreationTimeValue.ToVkDateTime();

        // ToDo: attachments

        [JsonProperty("geo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public GeoLocation Geo { get; set; }

        [JsonProperty("shared_post_text", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SharedPostText { get; set; }

        [JsonProperty("shared_post_creation_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long SharedPostCreationTimeValue { get; set; }

        [JsonProperty("signer_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long SignerId { get; set; }

        [JsonProperty("tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Tags { get; set; }

        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Author Author { get; set; }
    }
}
