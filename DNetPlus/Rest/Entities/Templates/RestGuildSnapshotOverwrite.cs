using Discord.API;

namespace Discord.Rest
{
    public class RestGuildSnapshotOverwrite
    {
        /// <summary>
        ///     Gets the unique identifier for the object this overwrite is targeting.
        /// </summary>
        public int TargetId { get; private set; }
        /// <summary>
        ///     Gets the type of object this overwrite is targeting.
        /// </summary>
        public PermissionTarget TargetType { get; private set; }
        /// <summary>
        ///     Gets the permissions associated with this overwrite entry.
        /// </summary>
        public OverwritePermissions Permissions { get; private set; }
        internal static RestGuildSnapshotOverwrite Create(GuildSnapshotOverwriteJson model)
        {
            return new RestGuildSnapshotOverwrite
            {
                TargetType = model.TargetType,
                TargetId = model.TargetId,
                Permissions = new OverwritePermissions
                {
                    DenyValue = model.Deny,
                    AllowValue = model.Allow
                }
            };
        }
    }
}
