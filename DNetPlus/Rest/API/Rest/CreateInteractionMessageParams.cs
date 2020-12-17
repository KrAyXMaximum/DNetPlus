using Newtonsoft.Json;

namespace Discord.API.Rest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class CreateInteractionMessageParams
    {
        [JsonProperty("type")]
        public InteractionMessageType Type { get; set; }
        [JsonProperty("data")]
        public CreateMessageParams Data { get; set; }
    }
    public enum InteractionMessageType
    {
        Pong = 1,
        Acknowledge = 2,
        ChannelMessage = 3,
        ChannelMessageWithSource = 4,
        AcknowledgeWithSource = 5
    }
}
