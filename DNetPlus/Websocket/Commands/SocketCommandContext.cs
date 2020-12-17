using Discord.WebSocket;
using System;

namespace Discord.Commands
{
    /// <summary>
    ///     Represents a WebSocket-based context of a command. This may include the client, guild, channel, user, and message.
    /// </summary>
    public class SocketCommandContext : ICommandContext
    {
        /// <summary>
        ///     Gets the <see cref="DiscordSocketClient" /> that the command is executed with.
        /// </summary>
        public DiscordSocketClient Client { get; }
        /// <summary>
        ///     Gets the <see cref="SocketGuild" /> that the command is executed in.
        /// </summary>
        public SocketGuild Guild { get; }
        /// <summary>
        ///     Gets the <see cref="ISocketMessageChannel" /> that the command is executed in.
        /// </summary>
        public ISocketMessageChannel Channel { get; }
        /// <summary>
        ///     Gets the <see cref="SocketUser" /> who executed the command.
        /// </summary>
        public SocketUser User { get; }
        /// <summary>
        ///     Gets the <see cref="SocketUserMessage" /> that the command is interpreted from.
        /// </summary>
        public SocketUserMessage Message { get; }

        /// <summary>
        ///     Indicates whether the channel that the command is executed in is a private channel.
        /// </summary>
        public bool IsPrivate => Channel is IPrivateChannel;

        public CommandInfo Command { get; set; }

        public string Prefix { get; set; }

        public InteractionData InteractionData { get; }

        /// <summary>
        ///     Initializes a new <see cref="SocketCommandContext" /> class with the provided client and message.
        /// </summary>
        /// <param name="client">The underlying client.</param>
        /// <param name="msg">The underlying message.</param>
        public SocketCommandContext(DiscordSocketClient client, SocketUserMessage msg)
        {
            Client = client;
            Guild = (msg.Channel as SocketGuildChannel)?.Guild;
            Channel = msg.Channel;
            User = msg.Author;
            Message = msg;
        }

        public SocketCommandContext(DiscordSocketClient client, Interaction interaction)
        {
            Client = client;
            Guild = interaction.Guild as SocketGuild;
            Channel = interaction.Channel as SocketTextChannel;
            User = interaction.Author as SocketUser;
            Message = new SocketUserMessage(client, 0, Channel, interaction.Author as SocketUser, MessageSource.User, CommandService.ParseInteractionData(interaction.Data));
            InteractionData = interaction.Data;
        }

        //ICommandContext
        /// <inheritdoc/>
        IDiscordClient ICommandContext.Client => Client;
        /// <inheritdoc/>
        IGuild ICommandContext.Guild => Guild;
        /// <inheritdoc/>
        IMessageChannel ICommandContext.Channel => Channel;
        /// <inheritdoc/>
        IUser ICommandContext.User => User;
        /// <inheritdoc/>
        IUserMessage ICommandContext.Message => Message;

        InteractionData ICommandContext.InteractionData => InteractionData;
    }
}
