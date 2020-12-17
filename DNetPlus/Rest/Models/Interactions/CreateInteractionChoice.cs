using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord
{
    public class CreateInteractionChoice
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
