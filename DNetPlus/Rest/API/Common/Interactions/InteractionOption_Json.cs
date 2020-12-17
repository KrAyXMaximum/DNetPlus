using Newtonsoft.Json;

namespace Discord.API
{
    public class InteractionOption_Json
    {
        [JsonProperty("type")]
        public InteractionOptionType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("options")]
        public InteractionOption_Json[] Options { get; set; }

        [JsonProperty("choices")]
        public InteractionChoice_Json[] Choices { get; set; }
    }
}
