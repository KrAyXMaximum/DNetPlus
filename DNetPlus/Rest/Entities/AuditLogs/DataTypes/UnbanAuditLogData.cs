using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to an unban.
    /// </summary>
    public class UnbanAuditLogData : IAuditLogData
    {
        private UnbanAuditLogData(IUser user)
        {
            Target = user;
        }

        internal static UnbanAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.UserJson userInfo = log.Users.FirstOrDefault(x => x.Id == entry.TargetId);
            return new UnbanAuditLogData(RestUser.Create(discord, userInfo));
        }

        /// <summary>
        ///     Gets the user that was unbanned.
        /// </summary>
        /// <returns>
        ///     A user object representing the user that was unbanned.
        /// </returns>
        public IUser Target { get; }
    }
}
