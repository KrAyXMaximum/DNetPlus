#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API.Gateway
{
    internal class GuildSyncEvent
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("large")]
        public bool Large { get; set; }

        [JsonProperty("presences")]
        public PresenceJson[] Presences { get; set; }
        [JsonProperty("members")]
        public GuildMemberJson[] Members { get; set; }
    }
}
