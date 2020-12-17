#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class RelationshipJson
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("user")]
        public UserJson User { get; set; }
        [JsonProperty("type")]
        public RelationshipType Type { get; set; }
    }
}
