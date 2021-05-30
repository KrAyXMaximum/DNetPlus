using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace TestBot
{
    public class Program
    {
        public static DiscordShardedClient Client;
        public static CommandService Commands;
        public static CommandHandler Handler;
        public static void Main(string[] args)
        {
            Start().GetAwaiter().GetResult();
        }
        public static async Task Start()
        {
            Client = new DiscordShardedClient(new DiscordSocketConfig
            {
                OwnerIds = new ulong[] { 190590364871032834 },
                GatewayIntents = Discord.GatewayIntents.Guilds | Discord.GatewayIntents.GuildMessages | Discord.GatewayIntents.GuildMembers,
                AlwaysDownloadUsers = false,
                LogLevel = Discord.LogSeverity.Debug
            });
            string File = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/DiscordBots/Boaty/Config.json";
            Client.Log += Client_Log;
            Client.UserJoinRequestDeleted += Client_UserJoinRequestDeleted;
            await Client.LoginAsync(Discord.TokenType.Bot, JObject.Parse(System.IO.File.ReadAllText(File))["Discord"].ToString());
            await Client.StartAsync();
            Client.InteractionReceived += Client_InteractionReceived;
            Commands = new CommandService();
            Handler = new CommandHandler(Client, Commands);
            await Handler.InstallCommandsAsync();
            await Task.Delay(-1);
        }

        private static async Task Client_UserJoinRequestDeleted(SocketUser arg1, SocketGuild arg2)
        {
            Console.WriteLine("GOT LEAVE");
            Console.WriteLine($"User: {arg1.Username} Guild: {arg2.Name}");
        }

        private static async Task Client_InteractionReceived(Interaction arg)
        {
            Console.WriteLine("Got interaction");
            switch (arg.Type)
            {
                case InteractionType.ApplicationCommand:
                    ShardedCommandContext context = new ShardedCommandContext(Client, arg);
                    _ = await Commands.ExecuteAsync(context: context, argPos: 0, services: null);
                    break;
                case InteractionType.MessageComponent:
                    if (arg.Data.CustomId == "boop")
                    {
                        Console.WriteLine("Boop!");
                        try
                        {
                            await (arg.Channel as ISocketMessageChannel).SendInteractionMessageAsync(arg.Data, $"<@{arg.User.Id}> You have been booped");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
            }
            
        }

        private static async Task Client_Log(Discord.LogMessage arg)
        {
            Console.WriteLine($"[{arg.Source}] {arg.Message}");
        }
    }
}
