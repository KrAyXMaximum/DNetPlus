#pragma warning disable CS1591
using Newtonsoft.Json;
using System;

namespace Discord.API
{
    internal class MessageJson
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("type")]
        public MessageType Type { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        // ALWAYS sent on WebSocket messages
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [JsonProperty("webhook_id")]
        public Optional<ulong> WebhookId { get; set; }
        [JsonProperty("author")]
        public Optional<UserJson> Author { get; set; }
        // ALWAYS sent on WebSocket messages
        [JsonProperty("member")]
        public Optional<GuildMemberJson> Member { get; set; }
        [JsonProperty("content")]
        public Optional<string> Content { get; set; }
        [JsonProperty("timestamp")]
        public Optional<DateTimeOffset> Timestamp { get; set; }
        [JsonProperty("edited_timestamp")]
        public Optional<DateTimeOffset?> EditedTimestamp { get; set; }
        [JsonProperty("tts")]
        public Optional<bool> IsTextToSpeech { get; set; }
        [JsonProperty("mention_everyone")]
        public Optional<bool> MentionEveryone { get; set; }
        [JsonProperty("mentions")]
        public Optional<EntityOrId<UserJson>[]> UserMentions { get; set; }
        [JsonProperty("mention_roles")]
        public Optional<ulong[]> RoleMentions { get; set; }
        [JsonProperty("attachments")]
        public Optional<AttachmentJson[]> Attachments { get; set; }
        [JsonProperty("embeds")]
        public Optional<EmbedJson[]> Embeds { get; set; }
        [JsonProperty("pinned")]
        public Optional<bool> Pinned { get; set; }
        [JsonProperty("reactions")]
        public Optional<ReactionJson[]> Reactions { get; set; }
        // sent with Rich Presence-related chat embeds
        [JsonProperty("activity")]
        public Optional<MessageActivityJson> Activity { get; set; }
        // sent with Rich Presence-related chat embeds
        [JsonProperty("application")]
        public Optional<MessageApplicationJson> Application { get; set; }
        [JsonProperty("message_reference")]
        public Optional<MessageReferenceJson> Reference { get; set; }
        [JsonProperty("flags")]
        public Optional<MessageFlags> Flags { get; set; }
        [JsonProperty("allowed_mentions")]
        public Optional<AllowedMentions> AllowedMentions { get; set; }

        [JsonProperty("stickers")]
        public Optional<StickerJson[]> Stickers { get; set; }
    }
}
