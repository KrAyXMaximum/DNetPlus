using Newtonsoft.Json;

namespace Discord.API
{
    public class WebhookFollowJson
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
