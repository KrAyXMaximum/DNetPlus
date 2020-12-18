using Discord.Net.Converters;
using Discord.Net.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Discord.API.Rest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class UploadInteractionFileParams
    {
        private static JsonSerializer _serializer = new JsonSerializer { ContractResolver = new DiscordContractResolver() };

        [JsonProperty("type")]
        public InteractionMessageType Type { get; set; }
        [JsonProperty("data")]
        public UploadWebhookFileParams Data { get; set; }

        public IReadOnlyDictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            string filename = Data.Filename.GetValueOrDefault("unknown.dat");
            if (Data.IsSpoiler && !filename.StartsWith(AttachmentExtensions.SpoilerPrefix))
                filename = filename.Insert(0, AttachmentExtensions.SpoilerPrefix);

            d["file"] = new MultipartFile(Data.File, filename);
            d["type"] = (int)Type;
            Dictionary<string, object> payload = new Dictionary<string, object>();
            
            if (Data.Content.IsSpecified)
                payload["content"] = Data.Content.Value;
            if (Data.IsTTS.IsSpecified)
                payload["tts"] = Data.IsTTS.Value.ToString();
            if (Data.Nonce.IsSpecified)
                payload["nonce"] = Data.Nonce.Value;
            if (Data.Username.IsSpecified)
                payload["username"] = Data.Username.Value;
            if (Data.AvatarUrl.IsSpecified)
                payload["avatar_url"] = Data.AvatarUrl.Value;
            if (Data.Embeds.IsSpecified)
                payload["embeds"] = Data.Embeds.Value;
            if (Data.AllowedMentions.IsSpecified)
                payload["allowed_mentions"] = Data.AllowedMentions.Value;

            StringBuilder json = new StringBuilder();
            using (StringWriter text = new StringWriter(json))
            using (JsonTextWriter writer = new JsonTextWriter(text))
                _serializer.Serialize(writer, payload);

            d["payload_json"] = json.ToString();
            return d;
        }
    }
}
