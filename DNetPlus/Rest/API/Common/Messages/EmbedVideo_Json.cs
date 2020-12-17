#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class EmbedVideoJson
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("height")]
        public Optional<int> Height { get; set; }
        [JsonProperty("width")]
        public Optional<int> Width { get; set; }
    }
}
