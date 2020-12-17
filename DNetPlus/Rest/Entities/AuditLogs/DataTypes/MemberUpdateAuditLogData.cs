using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to a change in a guild member.
    /// </summary>
    public class MemberUpdateAuditLogData : IAuditLogData
    {
        private MemberUpdateAuditLogData(IUser target, MemberInfo before, MemberInfo after)
        {
            Target = target;
            Before = before;
            After = after;
        }

        internal static MemberUpdateAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.AuditLogChangeJson[] changes = entry.Changes;

            API.AuditLogChangeJson nickModel = changes.FirstOrDefault(x => x.ChangedProperty == "nick");
            API.AuditLogChangeJson deafModel = changes.FirstOrDefault(x => x.ChangedProperty == "deaf");
            API.AuditLogChangeJson muteModel = changes.FirstOrDefault(x => x.ChangedProperty == "mute");

            string oldNick = nickModel?.OldValue?.ToObject<string>(discord.ApiClient.Serializer),
                newNick = nickModel?.NewValue?.ToObject<string>(discord.ApiClient.Serializer);
            bool? oldDeaf = deafModel?.OldValue?.ToObject<bool>(discord.ApiClient.Serializer),
                newDeaf = deafModel?.NewValue?.ToObject<bool>(discord.ApiClient.Serializer);
            bool? oldMute = muteModel?.OldValue?.ToObject<bool>(discord.ApiClient.Serializer),
                newMute = muteModel?.NewValue?.ToObject<bool>(discord.ApiClient.Serializer);

            API.UserJson targetInfo = log.Users.FirstOrDefault(x => x.Id == entry.TargetId);
            RestUser user = RestUser.Create(discord, targetInfo);

            MemberInfo before = new MemberInfo(oldNick, oldDeaf, oldMute);
            MemberInfo after = new MemberInfo(newNick, newDeaf, newMute);

            return new MemberUpdateAuditLogData(user, before, after);
        }

        /// <summary>
        ///     Gets the user that the changes were performed on.
        /// </summary>
        /// <returns>
        ///     A user object representing the user who the changes were performed on.
        /// </returns>
        public IUser Target { get; }
        /// <summary>
        ///     Gets the member information before the changes.
        /// </summary>
        /// <returns>
        ///     An information object containing the original member information before the changes were made.
        /// </returns>
        public MemberInfo Before { get; }
        /// <summary>
        ///     Gets the member information after the changes.
        /// </summary>
        /// <returns>
        ///     An information object containing the member information after the changes were made.
        /// </returns>
        public MemberInfo After { get; }
    }
}
