using Newtonsoft.Json;

namespace Discord.API
{
    internal class AllowedMentions
    {
        [JsonProperty("parse")]
        public Optional<string[]> Parse { get; set; }
        // Roles and Users have a max size of 100
        [JsonProperty("roles")]
        public Optional<ulong[]> Roles { get; set; }
        [JsonProperty("users")]
        public Optional<ulong[]> Users { get; set; }
    }
}
