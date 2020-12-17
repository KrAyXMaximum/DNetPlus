using Newtonsoft.Json;

namespace Discord.API
{
    public class InteractionChoice_Json
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
