#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    public class RoleTagsJson
    {
        [JsonProperty("bot_id")]
        public ulong? BotId { get; set; }
        [JsonProperty("integration_id")]
        public ulong? IntegrationId { get; set; }
        [JsonProperty("premium_subscriber")]
        public ulong? PremiumId { get; set; } = 0;
    }
}
