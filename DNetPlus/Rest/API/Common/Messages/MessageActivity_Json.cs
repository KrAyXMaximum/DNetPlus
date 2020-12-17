using Newtonsoft.Json;

namespace Discord.API
{
    public class MessageActivityJson
    {
        [JsonProperty("type")]
        public Optional<MessageActivityType> Type { get; set; }
        [JsonProperty("party_id")]
        public Optional<string> PartyId { get; set; }
    }
}
