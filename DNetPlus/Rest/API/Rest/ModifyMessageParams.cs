#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API.Rest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ModifyMessageParams
    {
        [JsonProperty("content")]
        public Optional<string> Content { get; set; }
        [JsonProperty("embed")]
        public Optional<EmbedJson> Embed { get; set; }
        [JsonProperty("allowed_mentions")]
        public Optional<AllowedMentions> AllowedMentions { get; set; }
        [JsonProperty("components")]
        public Optional<InteractionComponent_Json[]> Components { get; set; }
    }
}
