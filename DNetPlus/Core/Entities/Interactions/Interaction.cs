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
        public IChannel Channel { get; private set; }
        public InteractionData Data { get; private set;  }

        internal void Update(ClientState state, WebSocket.SocketGuild guild, InteractionCreateModel model)
        {
            Type = model.Type;
            Id = model.Id;
            if (guild != null)
            {
                Guild = guild;
                Member = guild.GetUser(model.Member.Value.User.Id) ?? guild.AddOrUpdateUser(model.Member.Value.User);
                User = state.GetUser(model.Member.Value.User.Id);
                Channel = guild.GetTextChannel(model.ChannelId);
            }
            else
            {
                Channel = state.GetChannel(model.ChannelId);
                User = state.GetUser(model.User.Value.Id);
            }
            
            Data = new InteractionData
            {
                Id = model.Id,
                Name = model.Data.Name,
                Token = model.Token,
                Choices = model.Data.Options == null ? new InteractionChoice[0] : model.Data.Options.Select(x => new InteractionChoice { Name = x.Name, Value = x.Value, Choices = GetChoices(x) }).ToArray()
            };
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
