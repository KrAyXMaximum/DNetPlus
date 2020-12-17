using Newtonsoft.Json;

namespace Discord.API
{
    internal class ReactionJson
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("me")]
        public bool Me { get; set; }
        [JsonProperty("emoji")]
        public EmojiJson Emoji { get; set; }
    }
}
