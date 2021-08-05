using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.API
{
    internal class InteractionComponent_Json
    {
        [JsonProperty("type")]
        public Discord.ComponentType Type { get; set; }
        [JsonProperty("custom_id")]
        public Optional<string> Id { get; set; }
        [JsonProperty("disabled")]
        public Optional<bool> Disabled { get; set; }
        [JsonProperty("style")]
        public Optional<ComponentButtonType> Style { get; set; }
        [JsonProperty("label")]
        public Optional<string> Label { get; set; }
        [JsonProperty("emoji")]
        public Optional<EmojiJson> Emoji { get; set; }
        [JsonProperty("url")]
        public Optional<string> Url { get; set; }
        [JsonProperty("options")]
        public Optional<InteractionSelectJson[]> SelectOptions { get; set; }
        [JsonProperty("placeholder")]
        public Optional<string> Placeholder { get; set; }
        [JsonProperty("min_values")]
        public Optional<int> MinValues { get; set; }
        [JsonProperty("max_values")]
        public Optional<int> MaxValues { get; set; }
        [JsonProperty("components")]
        public Optional<InteractionComponent_Json[]> Components { get; set; }
    }
    internal class InteractionSelectJson
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("emoji")]
        public Optional<EmojiJson> Emoji { get; set; }

        [JsonProperty("default")]
        public Optional<bool> Default { get; set; }
    }
}
