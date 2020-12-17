using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Discord.API
{
    internal class GuildTemplateJson
    {
        [JsonProperty("code")]
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonProperty("usage_count")]
        [JsonPropertyName("usage_count")]
        public int UsageCount { get; set; }
        [JsonProperty("creator_id")]
        [JsonPropertyName("creator_id")]
        public ulong CreatorId { get; set; }
        [JsonProperty("source_guild_id")]
        [JsonPropertyName("source_guild_id")]
        public ulong SourceGuildId { get; set; }
        [JsonProperty("creator")]
        [JsonPropertyName("creator")]
        public UserJson Creator { get; set; }

        [JsonProperty("created_at")]
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
    internal class GuildTemplateSnapshotJson : GuildTemplateJson
    {
        [JsonProperty("serialized_source_guild")]
        public GuildSnapshotJson Snapshot { get; set; }
    }
}
