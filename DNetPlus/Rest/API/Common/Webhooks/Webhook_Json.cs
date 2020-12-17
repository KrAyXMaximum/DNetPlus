#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class WebhookJson
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("name")]
        public Optional<string> Name { get; set; }
        [JsonProperty("avatar")]
        public Optional<string> Avatar { get; set; }
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }

        [JsonProperty("user")]
        public Optional<UserJson> Creator { get; set; }

        [JsonProperty("source_guild")]
        public Optional<WebhookFollowJson> SourceGuild { get; set; }
        [JsonProperty("source_channel")]
        public Optional<WebhookFollowJson> SourceChannel { get; set; }
    }
}
