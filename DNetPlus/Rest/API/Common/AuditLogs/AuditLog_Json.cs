using Newtonsoft.Json;

namespace Discord.API
{
    internal class AuditLogJson
    {
        [JsonProperty("webhooks")]
        public WebhookJson[] Webhooks { get; set; }

        [JsonProperty("users")]
        public UserJson[] Users { get; set; }

        [JsonProperty("audit_log_entries")]
        public AuditLogEntryJson[] Entries { get; set; }
    }
}
