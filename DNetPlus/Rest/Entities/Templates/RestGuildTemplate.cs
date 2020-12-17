using Discord.API;
using System;
using Model = Discord.API.GuildTemplateJson;

namespace Discord.Rest
{
    /// <inheritdoc />
    public class RestGuildTemplate : IGuildTemplate
    {
        /// <inheritdoc />
        public string Code { get; private set; }
        /// <inheritdoc />
        public string Name { get; private set; }
        /// <inheritdoc />
        public string Description { get; private set; }
        /// <inheritdoc />
        public int UsageCount { get; private set; }
        /// <inheritdoc />
        public ulong CreatorId { get; private set; }
        /// <inheritdoc />
        public IUser Creator { get; private set; }
        /// <inheritdoc />
        public DateTimeOffset CreatedAt { get; private set; }
        /// <inheritdoc />
        public DateTimeOffset UpdatedAt { get; private set; }
        /// <inheritdoc />
        public ulong SourceGuildId { get; private set; }
        /// <inheritdoc />
        public Optional<RestGuildSnapshot> Snapshot { get; private set; }

        internal static RestGuildTemplate Create(BaseDiscordClient discord, Model model, bool withSnapshot)
        {
            RestGuildTemplate entity = new RestGuildTemplate();
            entity.Update(discord, model, withSnapshot);
            return entity;
        }
        internal void Update(BaseDiscordClient discord, Model model, bool withSnapshot)
        {
            Code = model.Code;
            Name = model.Name;
            Description = model.Description;
            UsageCount = model.UsageCount;
            CreatorId = model.CreatorId;
            Creator = RestUser.Create(discord, model.Creator);
          
            SourceGuildId = model.SourceGuildId;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            if (withSnapshot)
                Snapshot = RestGuildSnapshot.Create(discord, (model as GuildTemplateSnapshotJson).Snapshot);
        }
    }
}
