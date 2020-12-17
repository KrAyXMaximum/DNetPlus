#pragma warning disable CS1591
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Discord.API
{
    internal class UserJson
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
        [JsonProperty("username")]
        [JsonPropertyName("username")]
        public Optional<string> Username { get; set; }

        [JsonProperty("discriminator")]
        [JsonPropertyName("discriminator")]
        public Optional<string> Discriminator { get; set; }

        [JsonProperty("bot")]
        [JsonPropertyName("bot")]
        public Optional<bool> Bot { get; set; }
        [JsonProperty("avatar")]
        [JsonPropertyName("avatar")]
        public Optional<string> Avatar { get; set; }

        //CurrentUser
        [JsonProperty("verified")]
        [JsonPropertyName("verified")]
        public Optional<bool> Verified { get; set; }
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public Optional<string> Email { get; set; }
        [JsonProperty("mfa_enabled")]
        [JsonPropertyName("mfa_enabled")]
        public Optional<bool> MfaEnabled { get; set; }
        [JsonProperty("flags")]
        [JsonPropertyName("flags")]
        public Optional<UserProperties> Flags { get; set; }
        [JsonProperty("premium_type")]
        [JsonPropertyName("premium_type")]
        public Optional<PremiumType> PremiumType { get; set; }
        [JsonProperty("locale")]
        [JsonPropertyName("locale")]
        public Optional<string> Locale { get; set; }
    }
}
