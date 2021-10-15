using Discord.API;
using Discord.API.Rest;
using Discord.Net.Converters;
using Discord.Rest;
using Discord.Webhook;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Discord
{
    public static class InteractionHelper
    {
        public static async Task<RestUserMessage> SendFollowupAsync(this InteractionData data, IMessageChannel channel, string text, bool isTTS = false, Embed embed = null, AllowedMentions allowedMentions = null, MessageReferenceParams reference = null, InteractionRow[] components = null, RequestOptions options = null)
        {
            Preconditions.AtMost(allowedMentions?.RoleIds?.Count ?? 0, 100, nameof(allowedMentions.RoleIds), "A max of 100 role Ids are allowed.");
            Preconditions.AtMost(allowedMentions?.UserIds?.Count ?? 0, 100, nameof(allowedMentions.UserIds), "A max of 100 user Ids are allowed.");

            // check that user flag and user Id list are exclusive, same with role flag and role Id list
            if (allowedMentions != null && allowedMentions.AllowedTypes.HasValue)
            {
                if (allowedMentions.AllowedTypes.Value.HasFlag(AllowedMentionTypes.Users) &&
                    allowedMentions.UserIds != null && allowedMentions.UserIds.Count > 0)
                {
                    throw new ArgumentException("The Users flag is mutually exclusive with the list of User Ids.", nameof(allowedMentions));
                }

                if (allowedMentions.AllowedTypes.Value.HasFlag(AllowedMentionTypes.Roles) &&
                    allowedMentions.RoleIds != null && allowedMentions.RoleIds.Count > 0)
                {
                    throw new ArgumentException("The Roles flag is mutually exclusive with the list of Role Ids.", nameof(allowedMentions));
                }
            }

            CreateWebhookMessageParams args = new CreateWebhookMessageParams(text)
            {
                IsTTS = isTTS,
                Embeds = embed != null ? new API.EmbedJson[] { embed?.ToModel(data.Client) } : Optional.Create<API.EmbedJson[]>(),
                AllowedMentions = allowedMentions != null ? allowedMentions?.ToModel() : Optional.Create<API.AllowedMentions>(),
                Components = components != null ? components?.Select(x => x.ToModel()).ToArray() : Optional.Create<InteractionComponent_Json[]>()
            
            };

            API.MessageJson model = await data.Client.ApiClient.CreateInteractionFollowupAsync(data.Token, args, options).ConfigureAwait(false);
            return RestUserMessage.Create(data.Client, channel, data.Client.CurrentUser, model);
        }
        
        public static async Task EditAsync(this InteractionData data, Action<MessageProperties> func, RequestOptions options = null)
        {
            var args = new MessageProperties();
            func(args);

            if (args.AllowedMentions.IsSpecified)
            {
                var allowedMentions = args.AllowedMentions.Value;
                Preconditions.AtMost(allowedMentions?.RoleIds?.Count ?? 0, 100, nameof(allowedMentions.RoleIds),
                    "A max of 100 role Ids are allowed.");
                Preconditions.AtMost(allowedMentions?.UserIds?.Count ?? 0, 100, nameof(allowedMentions.UserIds),
                    "A max of 100 user Ids are allowed.");

                // check that user flag and user Id list are exclusive, same with role flag and role Id list
                if (allowedMentions?.AllowedTypes != null)
                {
                    if (allowedMentions.AllowedTypes.Value.HasFlag(AllowedMentionTypes.Users) &&
                        allowedMentions.UserIds != null && allowedMentions.UserIds.Count > 0)
                    {
                        throw new ArgumentException("The Users flag is mutually exclusive with the list of User Ids.",
                            nameof(allowedMentions));
                    }

                    if (allowedMentions.AllowedTypes.Value.HasFlag(AllowedMentionTypes.Roles) &&
                        allowedMentions.RoleIds != null && allowedMentions.RoleIds.Count > 0)
                    {
                        throw new ArgumentException("The Roles flag is mutually exclusive with the list of Role Ids.",
                            nameof(allowedMentions));
                    }
                }
            }

            var apiArgs = new ModifyWebhookMessageParams
            {
                Content = args.Content.IsSpecified ? args.Content.Value : Optional.Create<string>(),
                Embeds =
                    args.Embed.IsSpecified
                        ? new API.EmbedJson[] { args.Embed.Value.ToModel(data.Client) }
                        : Optional.Create<API.EmbedJson[]>(),
                AllowedMentions = args.AllowedMentions.IsSpecified
                    ? args.AllowedMentions.Value.ToModel()
                    : Optional.Create<API.AllowedMentions>()
            };

            await data.Client.ApiClient.EditInteractionMessageAsync(data.Token, apiArgs, options).ConfigureAwait(false);
        }

        public static async Task DeleteAsync(this InteractionData data, RequestOptions options = null)
        {
            await data.Client.ApiClient.DeleteInteractionMessageAsync(data.Token, options).ConfigureAwait(false);
        }
    }
}
