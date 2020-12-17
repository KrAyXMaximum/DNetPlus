using Discord.API;
using System.Linq;

namespace Discord.Rest
{
    public class RestGuildSnapshotChannel
    {
        public int Id { get; private set; }
        public ChannelType Type { get; private set; }

        public Optional<string> Name { get; private set; }
        public Optional<int> Position { get; private set; }
        public Optional<RestGuildSnapshotOverwrite[]> PermissionOverwrites { get; private set; }
        public int? CategoryId { get; private set; }

        //TextChannel
        public Optional<string> Topic { get; private set; }

        public Optional<bool> Nsfw { get; private set; }
        public Optional<int> SlowMode { get; private set; }

        //VoiceChannel
        public Optional<int> Bitrate { get; private set; }
        public Optional<int> UserLimit { get; private set; }

        internal static RestGuildSnapshotChannel Create(GuildSnapshotChannelJson model)
        {
            RestGuildSnapshotChannel entity = new RestGuildSnapshotChannel
            {
                Bitrate = model.Bitrate,
                Id = int.Parse(model.Id.ToString()),
                Name = model.Name,
                Nsfw = model.Nsfw,
                Position = model.Position,
                SlowMode = model.SlowMode,
                Topic = model.Topic,
                Type = model.Type,
                UserLimit = model.UserLimit
            };
            if (model.CategoryId.HasValue)
                entity.CategoryId = model.CategoryId.Value;
            if (model.PermissionOverwrites.IsSpecified)
                entity.PermissionOverwrites = model.PermissionOverwrites.Value.Select(x => RestGuildSnapshotOverwrite.Create(x)).ToArray();

            return entity;
        }
    }
}
