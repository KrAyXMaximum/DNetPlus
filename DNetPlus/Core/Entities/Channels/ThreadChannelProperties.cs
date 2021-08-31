using System;

namespace Discord
{
    public class ThreadChannelProperties : GuildChannelProperties
    {
        public ulong Owner { get; set; }
        /// <summary>
        ///     Gets or sets the slow-mode ratelimit in seconds for this channel.
        /// </summary>
        /// <remarks>
        ///     Setting this value to anything above zero will require each user to wait X seconds before
        ///     sending another message; setting this value to <c>0</c> will disable slow-mode for this channel.
        ///     <note>
        ///         Users with <see cref="Discord.ChannelPermission.ManageMessages"/> or 
        ///         <see cref="ChannelPermission.ManageChannels"/> will be exempt from slow-mode.
        ///     </note>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value does not fall within [0, 21600].</exception>
        public Optional<int> SlowModeInterval { get; set; }

        public Optional<ThreadMetadataProperties> ThreadMetadata { get; set; }
    }
    public class ThreadMetadataProperties
    {

    }
}
