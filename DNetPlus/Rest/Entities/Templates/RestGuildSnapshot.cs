using System.Collections.Generic;
using System.Collections.Immutable;
using Model = Discord.API.GuildSnapshotJson;

namespace Discord.Rest
{
    public class RestGuildSnapshot
    {
        private ImmutableDictionary<int, RestGuildSnapshotRole> _roles;
        public IReadOnlyCollection<RestGuildSnapshotRole> Roles => _roles.ToReadOnlyCollection();
        private ImmutableDictionary<int, RestGuildSnapshotChannel> _channels;
        public IReadOnlyCollection<RestGuildSnapshotChannel> Channels => _channels.ToReadOnlyCollection();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Region { get; private set; }
        public string IconHash { get; private set; }
        public VerificationLevel VerificationLevel { get; private set; }
        public DefaultMessageNotifications DefaultMessageNotifications { get; private set; }

        public ExplicitContentFilterLevel ExplicitContentFilter { get; private set; }
        public string PreferredLocale { get; private set; }
        public int AFKTimeout { get; private set; }
        
        public int? AFKChannelId { get; private set; }
        public int? SystemChannelId { get; private set; }
        public SystemChannelMessageDeny SystemChannelFlags { get; private set; }

        internal static RestGuildSnapshot Create(BaseDiscordClient discord, Model model)
        {
            RestGuildSnapshot entity = new RestGuildSnapshot
            {
                Name = model.Name,
                AFKTimeout = model.AFKTimeout,
                DefaultMessageNotifications = model.DefaultMessageNotifications,
                Description = model.Description,
                ExplicitContentFilter = model.ExplicitContentFilter,
                IconHash = model.IconHash,
                PreferredLocale = model.PreferredLocale,
                Region = model.Region,
                SystemChannelFlags = model.SystemChannelFlags,
                VerificationLevel = model.VerificationLevel
            };
            if (model.AFKChannelId.HasValue)
                entity.AFKChannelId = model.AFKChannelId.Value;
            if (model.SystemChannelId.HasValue)
                entity.SystemChannelId = model.SystemChannelId.Value;

            ImmutableDictionary<int, RestGuildSnapshotRole>.Builder roles = ImmutableDictionary.CreateBuilder<int, RestGuildSnapshotRole>();
            if (model.Roles != null)
            {
                for (int i = 0; i < model.Roles.Length; i++)
                    roles[model.Roles[i].Id] = RestGuildSnapshotRole.Create(model.Roles[i]);
            }
            entity._roles = roles.ToImmutable();

            ImmutableDictionary<int, RestGuildSnapshotChannel>.Builder channels = ImmutableDictionary.CreateBuilder<int, RestGuildSnapshotChannel>();
            if (model.Channels != null)
            {
                for (int i = 0; i < model.Channels.Length; i++)
                    channels[model.Channels[i].Id] = RestGuildSnapshotChannel.Create(model.Channels[i]);
            }
            entity._channels = channels.ToImmutable();

            return entity;
        }
    }
}
