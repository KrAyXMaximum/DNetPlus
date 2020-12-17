using Discord.WebSocket;

namespace Discord.Commands
{
    /// <summary> The sharded variant of <see cref="ICommandContext"/>, which may contain the client, user, guild, channel, and message. </summary>
    public class ShardedCommandContext : SocketCommandContext, ICommandContext
    {
        /// <summary> Gets the <see cref="DiscordShardedClient"/> that the command is executed with. </summary>
        public new DiscordShardedClient Client { get; }

        public InteractionData InteractionData { get; }

        public ShardedCommandContext(DiscordShardedClient client, SocketUserMessage msg)
            : base(client.GetShardIndex(GetShardId(client, (msg.Channel as SocketGuildChannel)?.Guild)), msg)
        {
            Client = client;
        }

        public ShardedCommandContext(DiscordShardedClient client, Interaction interaction)
            : base(client.GetShardIndex(GetShardId(client, interaction.Guild)), interaction)
        {
            Client = client;
            InteractionData = interaction.Data;
        }

        /// <summary> Gets the shard ID of the command context. </summary>
        private static int GetShardId(DiscordShardedClient client, IGuild guild)
            => guild == null ? 0 : client.GetShardIdFor(guild);

        //ICommandContext
        /// <inheritdoc />
        IDiscordClient ICommandContext.Client => Client;

        InteractionData ICommandContext.InteractionData => InteractionData;
    }
}
