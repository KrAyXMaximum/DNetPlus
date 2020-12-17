using System.Collections.Generic;
using System.Linq;
using InteractionCreateModel = Discord.API.Gateway.InteractionCreateJson;

namespace Discord
{
    public class Interaction
    {
        public InteractionType Type { get; private set; }
        public IGuildUser Author { get; private set; }
        public ulong Id { get; private set; }
        public IGuild Guild { get; private set; }
        public ITextChannel Channel { get; private set; }
        public InteractionData Data { get; private set;  }

        internal void Update(WebSocket.SocketGuild guild, InteractionCreateModel model)
        {
            Type = model.Type;
            Author = guild.GetUser(model.Author.User.Id) ?? guild.AddOrUpdateUser(model.Author.User);
            Id = model.Id;
            Guild = guild;
            Channel = guild.GetTextChannel(model.ChannelId);
            Data = new InteractionData
            {
                Id = model.Id,
                Name = model.Data.Name,
                Token = model.Token,
                Choices = model.Data.Options == null ? new InteractionChoice[0] : model.Data.Options.Select(x => new InteractionChoice { Name = x.Name, Value = x.Value }).ToArray()
            };
        }
    }
    public enum InteractionType
    {
        Ping = 1,
        ApplicationCommand = 2
    }
    public class InteractionData
    {
        public InteractionChoice[] Choices { get; internal set; }
        public string Name { get; internal set; }
        public ulong Id { get; internal set; }

        public string Token { get; internal set; }
    }
    
}
