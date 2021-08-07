#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API.Gateway
{
    internal class MessageDeleteBulkEvent
    {
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("ids")]
        public ulong[] Ids { get; set; }
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
    }
}
