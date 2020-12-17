using Newtonsoft.Json;

namespace Discord.API
{
    internal class MessageReferenceJson
    {
        [JsonProperty("message_id")]
        public Optional<ulong> MessageId { get; set; }

        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }

        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
    }
}
