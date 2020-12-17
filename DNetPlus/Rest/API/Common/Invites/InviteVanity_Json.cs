using Newtonsoft.Json;

namespace Discord.API
{
    public class InviteVanityJson
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
