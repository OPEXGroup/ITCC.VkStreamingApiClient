// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.VkStreamingApiClient.Utils;
using Newtonsoft.Json;

namespace ITCC.VkStreamingApiClient.Models.Entities
{
    public class Place
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }

        [JsonProperty("created", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long CreatedValue { get; set; }
        [JsonIgnore]
        public DateTime? Created => CreatedValue.ToVkDateTime();

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        // For checkins

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Type { get; set; }

        [JsonProperty("group_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long GroupId { get; set; }

        [JsonProperty("group_photo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GroupPhotoUrl { get; set; }

        [JsonProperty("checkins", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Checkins { get; set; }

        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long UpdatedValue { get; set; }
        [JsonIgnore]
        public DateTime? Updated => UpdatedValue.ToVkDateTime();

        [JsonProperty("address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Address { get; set; }
    }
}
