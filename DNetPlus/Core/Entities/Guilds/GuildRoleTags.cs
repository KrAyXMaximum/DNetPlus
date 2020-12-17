using Discord.API;

namespace Discord
{
    public class GuildRoleTags
    {
        public GuildRoleTags(RoleTagsJson tags)
        {
            if (tags != null)
            {
                IntegrationId = tags.IntegrationId.HasValue ? tags.IntegrationId.Value : Optional.Create<ulong>();
                BotId = tags.BotId.HasValue ? tags.BotId.Value : Optional.Create<ulong>();
                IsBoosterRole = !tags.PremiumId.HasValue;
            }
        }
        public Optional<ulong> IntegrationId { get; private set; }
        public Optional<ulong> BotId { get; private set; }
        public bool IsBoosterRole { get; private set; }
    }
}
