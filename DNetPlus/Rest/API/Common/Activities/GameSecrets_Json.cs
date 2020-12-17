using Newtonsoft.Json;

namespace Discord.API
{
    internal class GameSecretsJson
    {
        [JsonProperty("match")]
        public string Match { get; set; }
        [JsonProperty("join")]
        public string Join { get; set; }
        [JsonProperty("spectate")]
        public string Spectate { get; set; }
    }
}