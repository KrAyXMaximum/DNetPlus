using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using InteractionCreateModel = Discord.API.Gateway.InteractionCreateJson;

namespace Discord
{
    public class Interaction
    {
        public InteractionType Type { get; private set; }
        public IGuildUser? Member { get; private set; }
        public IUser? User { get; private set; }
        public ulong Id { get; private set; }
        public IGuild? Guild { get; private set; }
        public ISocketMessageChannel Channel { get; private set; }
        public ulong? MessageId { get; private set; }
        public ulong ChannelId { get; private set; }
        public InteractionData Data { get; private set;  }

        internal void Update(DiscordSocketClient client, ClientState state, WebSocket.SocketGuild guild, ISocketMessageChannel channel, SocketUser user, InteractionCreateModel model)
        {
            Type = model.Type;
            Id = model.Id;
            if (model.Message.IsSpecified)
            MessageId = model.Message.Value.Id;

            ChannelId = channel.Id;
            Channel = channel;
            if (guild != null)
            {
                Guild = guild;
                Member = guild.GetUser(model.Member.Value.User.Id);
            }
            User = user;
            Data = new InteractionData(client)
            {
                Id = model.Id,
                Name = model.Data.Name.IsSpecified ? model.Data.Name.Value : null,
                Token = model.Token,
                Choices = model.Data.Options == null ? new InteractionChoice[0] : model.Data.Options.Select(x => new InteractionChoice { Name = x.Name, Value = x.Value, Choices = GetChoices(x) }).ToArray(),
                ComponentType = model.Data.ComponentType,
                CustomId = model.Data.CustomId.IsSpecified ? model.Data.CustomId.Value : null,
                DropdownOptions = model.Data.DropdownValues.IsSpecified ? model.Data.DropdownValues.Value : null
            };
            if (model.Data.Resolved.IsSpecified)
            {
                Data.Resolve = new InteractionResolved
                {
                    Users = model.Data.Resolved.Value.Users.IsSpecified ? model.Data.Resolved.Value.Users.Value.ToDictionary(x => x.Key, x => (SocketUser)SocketUnknownUser.Create(client, state, x.Value)) : new Dictionary<string, SocketUser>()
                };
                if (model.Data.Resolved.Value.Members.IsSpecified)
                {
                    foreach(var m in model.Data.Resolved.Value.Members.Value)
                    {
                        if (m.Value.User == null)
                            m.Value.User = model.Data.Resolved.Value.Users.Value[m.Key];
                        Data.Resolve.Members.Add(m.Key, SocketGuildUser.Create(guild, state, m.Value));
                    }
                }
                if (model.Data.Resolved.Value.Messages.IsSpecified)
                    Data.Resolve.Messages = model.Data.Resolved.Value.Messages.IsSpecified ? model.Data.Resolved.Value.Messages.Value.ToDictionary(x => x.Key, x => SocketMessage.Create(client, state, x.Value.Author.IsSpecified ? state.GetUser(x.Value.Author.Value.Id) : null, (ISocketMessageChannel)state.GetChannel(x.Value.ChannelId), x.Value)) : new Dictionary<string, SocketMessage>();
            }
        }

        internal InteractionChoice[] GetChoices(API.Gateway.InteractionOptionJson option)
        {
            if (option.Options != null && option.Options.Any())
                return option.Options.Select(x => new InteractionChoice { Name = x.Name, Value = x.Value, Choices = GetChoices(x) }).ToArray();
          return new InteractionChoice[0];
        }
    }
    public enum InteractionType
    {
        Ping = 1,
        ApplicationCommand = 2,
        MessageComponent = 3
    }
    public class InteractionData
    {
        public InteractionData(BaseDiscordClient client)
        {
            Client = client;
        }
        internal BaseDiscordClient Client;
        public string CustomId { get; internal set; }
        public ComponentType ComponentType { get; internal set; }
        public InteractionChoice[] Choices { get; internal set; } = new InteractionChoice[0];
        public InteractionResolved Resolve { get; internal set; } = new InteractionResolved();
        public string[] DropdownOptions { get; set; } = new string[0];
        public string Name { get; internal set; }
        public ulong Id { get; internal set; }
        public string Token { get; internal set; }
        internal bool IsFollowup;
    }
    public class InteractionResolved
    {
        public Dictionary<string, SocketGuildUser> Members { get; set; } = new Dictionary<string, SocketGuildUser>();
        public Dictionary<string, SocketUser> Users { get; set; } = new Dictionary<string, SocketUser>();

        public Dictionary<string, SocketMessage> Messages { get; set; } = new Dictionary<string, SocketMessage>();
    }
}
