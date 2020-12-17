#pragma warning disable CS1591
using System;
using Newtonsoft.Json;
using Discord.Net.Converters;

namespace Discord.API
{
    internal class EmbedJson
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("color")]
        public uint? Color { get; set; }
        [JsonProperty("type"), JsonConverter(typeof(EmbedTypeConverter))]
        public EmbedType Type { get; set; }
        [JsonProperty("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }
        [JsonProperty("author")]
        public Optional<EmbedAuthorJson> Author { get; set; }
        [JsonProperty("footer")]
        public Optional<EmbedFooterJson> Footer { get; set; }
        [JsonProperty("video")]
        public Optional<EmbedVideoJson> Video { get; set; }
        [JsonProperty("thumbnail")]
        public Optional<EmbedThumbnailJson> Thumbnail { get; set; }
        [JsonProperty("image")]
        public Optional<EmbedImageJson> Image { get; set; }
        [JsonProperty("provider")]
        public Optional<EmbedProviderJson> Provider { get; set; }
        [JsonProperty("fields")]
        public Optional<EmbedFieldJson[]> Fields { get; set; }
    }
}
