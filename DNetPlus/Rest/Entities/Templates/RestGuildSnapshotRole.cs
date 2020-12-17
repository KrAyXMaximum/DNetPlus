using Discord.API;

namespace Discord.Rest
{
    public class RestGuildSnapshotRole
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public uint Color { get; private set; }
        public bool Hoist { get; private set; }
        public bool Mentionable { get; private set; }
        public ulong Permissions { get; private set; }

        internal static RestGuildSnapshotRole Create(GuildSnapshotRoleJson role)
        {
            RestGuildSnapshotRole entity = new RestGuildSnapshotRole
            {
                Color = role.Color,
                Hoist = role.Hoist,
                Id = int.Parse(role.Id.ToString()),
                Mentionable = role.Mentionable,
                Name = role.Name,
                Permissions = role.Permissions
            };
            return entity;
        }
    }
}
