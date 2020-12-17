using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to a ban.
    /// </summary>
    public class BanAuditLogData : IAuditLogData
    {
        private BanAuditLogData(IUser user)
        {
            Target = user;
        }

        internal static BanAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.UserJson userInfo = log.Users.FirstOrDefault(x => x.Id == entry.TargetId);
            return new BanAuditLogData(RestUser.Create(discord, userInfo));
        }

        /// <summary>
        ///     Gets the user that was banned.
        /// </summary>
        /// <returns>
        ///     A user object representing the banned user.
        /// </returns>
        public IUser Target { get; }
    }
}
