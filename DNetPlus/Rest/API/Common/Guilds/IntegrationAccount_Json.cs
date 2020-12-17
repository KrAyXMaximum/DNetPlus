#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class IntegrationAccountJson
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
