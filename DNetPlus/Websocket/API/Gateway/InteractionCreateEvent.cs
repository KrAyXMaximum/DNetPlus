using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.API.Gateway
{
    internal class InteractionCreateJson
    {
        [JsonProperty("type")]
        public InteractionType Type { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("message")]
        public Optional<MessageJson> Message { get; set; }

        [JsonProperty("member")]
        public Optional<GuildMemberJson> Member { get; set; }
        [JsonProperty("user")]
        public Optional<UserJson> User { get; set; }
       
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [JsonProperty("data")]
        public InteractionDataJson Data { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        
    }
    internal class InteractionDataJson
    {
        [JsonProperty("custom_id")]
        public Optional<string> CustomId { get; set; }
        [JsonProperty("disabled")]
        public Optional<bool> Disabled { get; set; }
        [JsonProperty("component_type")]
        public ComponentType ComponentType { get; set; }
        [JsonProperty("options")]
        public InteractionOptionJson[] Options { get; set; }
        [JsonProperty("values")]
        public Optional<string[]> DropdownValues { get; set; }
        [JsonProperty("name")]
        public Optional<string> Name { get; set; }
        [JsonProperty("id")]
        public Optional<ulong> Id { get; set; }
        [JsonProperty("resolved")]
        public Optional<InteractionResolvedJson> Resolved { get; set; }
    }
    internal class InteractionOptionJson
    {
        [JsonProperty("options")]
        public InteractionOptionJson[] Options { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
    internal class InteractionResolvedJson
    {
        [JsonProperty("members")]
        public Optional<Dictionary<string, GuildMemberJson>> Members { get; set; }
        [JsonProperty("users")]
        public Optional<Dictionary<string, UserJson>> Users { get; set; }
        [JsonProperty("messages")]
        public Optional<Dictionary<string, MessageJson>> Messages { get; set; }
    }
}
