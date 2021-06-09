using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord
{
    public class CreateInteraction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("options")]
        public CreateInteractionOption[] Options { get; set; } 
    }
}
