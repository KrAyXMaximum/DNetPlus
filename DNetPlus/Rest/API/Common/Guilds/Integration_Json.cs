#pragma warning disable CS1591
using Newtonsoft.Json;
using System;

namespace Discord.API
{
    internal class IntegrationJson
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("syncing")]
        public bool Syncing { get; set; }
        [JsonProperty("role_id")]
        public ulong RoleId { get; set; }
        [JsonProperty("expire_behavior")]
        public ulong ExpireBehavior { get; set; }
        [JsonProperty("expire_grace_period")]
        public ulong ExpireGracePeriod { get; set; }
        [JsonProperty("user")]
        public UserJson User { get; set; }
        [JsonProperty("account")]
        public IntegrationAccountJson Account { get; set; }
        [JsonProperty("synced_at")]
        public DateTimeOffset SyncedAt { get; set; }
    }
}
