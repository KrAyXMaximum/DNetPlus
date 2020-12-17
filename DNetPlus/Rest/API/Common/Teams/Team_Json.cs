#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class TeamJson
    {
        [JsonProperty("icon")]
        public Optional<string> Icon { get; set; }
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("members")]
        public TeamMemberJson[] TeamMembers { get; set; }
        [JsonProperty("owner_user_id")]
        public ulong OwnerUserId { get; set; }
    }
}