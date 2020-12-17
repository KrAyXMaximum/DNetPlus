#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class TeamMemberJson
    {
        [JsonProperty("membership_state")]
        public MembershipState MembershipState { get; set; }
        [JsonProperty("permissions")]
        public string[] Permissions { get; set; }
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }
        [JsonProperty("user")]
        public UserJson User { get; set; }
    }
}