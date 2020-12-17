using Newtonsoft.Json;
using System.Linq;

namespace Discord.API
{
    internal class Interaction_Json
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("application_id")]
        public ulong ApplicationId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("options")]
        public InteractionOption_Json[] Options { get; set; }
    }
}
