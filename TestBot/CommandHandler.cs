using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TestBot
{
    public class CommandHandler
    {
        private readonly DiscordShardedClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordShardedClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            SocketUserMessage message = messageParam as SocketUserMessage;
            if (message == null || message.Author.IsBot) 
                return;
            int argPos = 0;
            if (!message.HasStringPrefix("tb/", ref argPos))
                return;
            SocketCommandContext context = new ShardedCommandContext(_client, message);
            _ = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);

        }
    }
}
