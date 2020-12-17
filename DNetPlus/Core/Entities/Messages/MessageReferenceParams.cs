using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Not available yet.
    /// </summary>
    public class MessageReferenceParams
    {
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("message_id")]
        public ulong MessageId { get; set; }
    }
}
