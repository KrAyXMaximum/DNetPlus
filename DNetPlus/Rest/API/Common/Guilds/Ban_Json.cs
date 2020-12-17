#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class BanJson
    {
        [JsonProperty("user")]
        public UserJson User { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
