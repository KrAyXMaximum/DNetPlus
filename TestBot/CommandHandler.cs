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
        public readonly IServiceProvider _services;
        public CommandHandler(DiscordShardedClient client, CommandService commands, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;
            _commands.CommandExecuted += _commands_CommandExecuted;
        }

        private async Task _commands_CommandExecuted(Discord.Optional<CommandInfo> arg1, ICommandContext arg2, IResult arg3)
        {
           if (!arg3.IsSuccess)
           {
                Console.WriteLine("COMMAND: " + arg3.ErrorReason);
            }
        }


        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetExecutingAssembly(), services: _services);
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _services);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            Console.WriteLine($"{messageParam.Author.Username}: {messageParam.Content}");
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
                services: _services);

        }
    }
}
