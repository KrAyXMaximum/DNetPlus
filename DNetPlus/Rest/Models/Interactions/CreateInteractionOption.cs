using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord
{
    public class CreateInteractionOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("type")]
        public InteractionOptionType Type { get; set; }

        [JsonProperty("options")]
        public CreateInteractionOption[] Options { get; set; }

        [JsonProperty("choices")]
        public CreateInteractionChoice[] Choices { get; set; }
    }
}
