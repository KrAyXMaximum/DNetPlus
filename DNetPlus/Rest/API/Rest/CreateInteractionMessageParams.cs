using Newtonsoft.Json;

namespace Discord.API.Rest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class CreateInteractionMessageParams
    {
        [JsonProperty("type")]
        public InteractionMessageType Type { get; set; }
        [JsonProperty("data")]
        public CreateWebhookMessageParams Data { get; set; }
    }
    public enum InteractionMessageType
    {
        Pong = 1,
        ChannelMessageWithSource = 4,
        AcknowledgeWithSource = 5,
        DeferredUpdateMessage = 6,
        UpdateMessage = 7
    }
}
