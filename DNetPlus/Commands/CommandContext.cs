
using Discord.WebSocket;

namespace Discord.Commands
{
    /// <summary> The context of a command which may contain the client, user, guild, channel, and message. </summary>
    public class CommandContext : ICommandContext
    {
        /// <inheritdoc/>
        public IDiscordClient Client { get; }
        /// <inheritdoc/>
        public IGuild Guild { get; }
        /// <inheritdoc/>
        public IMessageChannel Channel { get; }
        /// <inheritdoc/>
        public IUser User { get; }
        /// <inheritdoc/>
        public IGuildUser GuildUser { get; }
        /// <inheritdoc/>
        public IUserMessage Message { get; }

        public CommandInfo Command { get; set; }

        public string Prefix { get; set; }

        /// <summary> Indicates whether the channel that the command is executed in is a private channel. </summary>
        public bool IsPrivate => Channel is IPrivateChannel;

        public InteractionData InteractionData { get; set; }

        /// <summary>
        ///     Initializes a new <see cref="CommandContext" /> class with the provided client and message.
        /// </summary>
        /// <param name="client">The underlying client.</param>
        /// <param name="msg">The underlying message.</param>
        public CommandContext(IDiscordClient client, IUserMessage msg)
        {
            Client = client;
            Guild = (msg.Channel as IGuildChannel)?.Guild;
            Channel = msg.Channel;
            User = msg.Author;
            if (Guild != null)
                GuildUser = msg.Author as IGuildUser;
            Message = msg;
        }

        public CommandContext(DiscordSocketClient client, Interaction interaction)
        {
            Client = client;
            Guild = interaction.Guild;
            Channel = interaction.Channel;
            User = interaction.Member ?? interaction.User;
            if (Guild != null)
                GuildUser = interaction.Member;
            Message = new SocketUserMessage(client, interaction.MessageId.HasValue ? interaction.MessageId.Value : 0, interaction.Channel as ISocketMessageChannel, User as SocketUser, MessageSource.User);
            InteractionData = interaction.Data;
        }
    }
}
