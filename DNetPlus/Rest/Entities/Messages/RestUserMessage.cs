using Discord.API;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading.Tasks;
using Model = Discord.API.MessageJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Represents a REST-based message sent by a user.
    /// </summary>
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public class RestUserMessage : RestMessage, IUserMessage
    {
        private bool _isMentioningEveryone, _isTTS, _isPinned, _isSuppressed;
        private long? _editedTimestampTicks;
        private ImmutableArray<Attachment> _attachments = ImmutableArray.Create<Attachment>();
        private ImmutableArray<Embed> _embeds = ImmutableArray.Create<Embed>();
        private ImmutableArray<MessageSticker> _stickers = ImmutableArray.Create<MessageSticker>();
        private ImmutableArray<InteractionRow> _components = ImmutableArray.Create<InteractionRow>();
        private ImmutableArray<ITag> _tags = ImmutableArray.Create<ITag>();

        /// <inheritdoc />
        public override bool IsTTS => _isTTS;
        /// <inheritdoc />
        public override bool IsPinned => _isPinned;
        /// <inheritdoc />
        public override bool IsSuppressed => _isSuppressed;
        /// <inheritdoc />
        public override DateTimeOffset? EditedTimestamp => DateTimeUtils.FromTicks(_editedTimestampTicks);
        /// <inheritdoc />
        public override IReadOnlyCollection<Attachment> Attachments => _attachments;
        /// <inheritdoc />
        public override IReadOnlyCollection<Embed> Embeds => _embeds;
        /// <inheritdoc />
        public override IReadOnlyCollection<ulong> MentionedChannelIds => MessageHelper.FilterTagsByKey(TagType.ChannelMention, _tags);
        /// <inheritdoc />
        public override IReadOnlyCollection<ulong> MentionedRoleIds => MessageHelper.FilterTagsByKey(TagType.RoleMention, _tags);
        /// <inheritdoc />
        public override IReadOnlyCollection<RestUser> MentionedUsers => MessageHelper.FilterTagsByValue<RestUser>(TagType.UserMention, _tags);
        /// <inheritdoc />
        public override IReadOnlyCollection<ITag> Tags => _tags;

        public override IReadOnlyCollection<MessageSticker> Stickers => _stickers;
        public override IReadOnlyCollection<InteractionRow> Components => _components;

        internal RestUserMessage(BaseDiscordClient discord, ulong id, IMessageChannel channel, IUser author, MessageSource source)
            : base(discord, id, channel, author, source)
        {
        }
        internal new static RestUserMessage Create(BaseDiscordClient discord, IMessageChannel channel, IUser author, Model model)
        {
            RestUserMessage entity = new RestUserMessage(discord, model.Id, channel, author, MessageHelper.GetSource(model));
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);

            if (model.IsTextToSpeech.IsSpecified)
                _isTTS = model.IsTextToSpeech.Value;
            if (model.Pinned.IsSpecified)
                _isPinned = model.Pinned.Value;
            if (model.EditedTimestamp.IsSpecified)
                _editedTimestampTicks = model.EditedTimestamp.Value?.UtcTicks;
            if (model.MentionEveryone.IsSpecified)
                _isMentioningEveryone = model.MentionEveryone.Value;
            if (model.Flags.IsSpecified)
            {
                _isSuppressed = model.Flags.Value.HasFlag(MessageFlags.SuppressEmbeds);
            }

            if (model.Attachments.IsSpecified)
            {
                API.AttachmentJson[] value = model.Attachments.Value;
                if (value.Length > 0)
                {
                    ImmutableArray<Attachment>.Builder attachments = ImmutableArray.CreateBuilder<Attachment>(value.Length);
                    for (int i = 0; i < value.Length; i++)
                        attachments.Add(Attachment.Create(value[i]));
                    _attachments = attachments.ToImmutable();
                }
                else
                    _attachments = ImmutableArray.Create<Attachment>();
            }

            if (model.Embeds.IsSpecified)
            {
                API.EmbedJson[] value = model.Embeds.Value;
                if (value.Length > 0)
                {
                    ImmutableArray<Embed>.Builder embeds = ImmutableArray.CreateBuilder<Embed>(value.Length);
                    for (int i = 0; i < value.Length; i++)
                        embeds.Add(value[i].ToEntity());
                    _embeds = embeds.ToImmutable();
                }
                else
                    _embeds = ImmutableArray.Create<Embed>();
            }

            if (model.Stickers.IsSpecified)
            {
                StickerJson[] value = model.Stickers.Value;
                if (value.Length > 0)
                {
                    ImmutableArray<MessageSticker>.Builder stickers = ImmutableArray.CreateBuilder<MessageSticker>(value.Length);
                    for (int i = 0; i < value.Length; i++)
                        stickers.Add(value[i].ToEntity());
                    _stickers = stickers.ToImmutable();
                }
                else
                    _stickers = ImmutableArray.Create<MessageSticker>();
            }

                if (model.Components.IsSpecified)
                {
                    InteractionComponent_Json[] value = model.Components.Value;
                    if (value.Length > 0)
                    {
                        ImmutableArray<InteractionRow>.Builder buttons = ImmutableArray.CreateBuilder<InteractionRow>(value.Length);
                        for (int i = 0; i < value.Length; i++)
                            buttons.Add(value[i].ToEntity());
                        _components = buttons.ToImmutable();
                    }
                    else
                        _components = ImmutableArray.Create<InteractionRow>();
                }
            ImmutableArray<IUser> mentions = ImmutableArray.Create<IUser>();
            if (model.UserMentions.IsSpecified)
            {
                EntityOrId<UserJson>[] value = model.UserMentions.Value;
                if (value.Length > 0)
                {
                    ImmutableArray<IUser>.Builder newMentions = ImmutableArray.CreateBuilder<IUser>(value.Length);
                    for (int i = 0; i < value.Length; i++)
                    {
                        EntityOrId<UserJson> val = value[i];
                        if (val.Object != null)
                            newMentions.Add(RestUser.Create(Discord, val.Object));
                    }
                    mentions = newMentions.ToImmutable();
                }
            }

            if (model.Content.IsSpecified)
            {
                string text = model.Content.Value;
                ulong? guildId = (Channel as IGuildChannel)?.GuildId;
                IGuild guild = guildId != null ? (Discord as IDiscordClient).GetGuildAsync(guildId.Value, CacheMode.CacheOnly).Result : null;
                _tags = MessageHelper.ParseTags(text, null, guild, mentions);
                model.Content = text;
            }
        }

        /// <inheritdoc />
        public async Task ModifyAsync(Action<MessageProperties> func, RequestOptions options = null)
        {
            Model model = await MessageHelper.ModifyAsync(this, Discord, func, options).ConfigureAwait(false);
            Update(model);
        }

        /// <inheritdoc />
        public Task PinAsync(RequestOptions options = null)
            => MessageHelper.PinAsync(this, Discord, options);
        /// <inheritdoc />
        public Task UnpinAsync(RequestOptions options = null)
            => MessageHelper.UnpinAsync(this, Discord, options);
        /// <inheritdoc />

        public string Resolve(int startIndex, TagHandling userHandling = TagHandling.Name, TagHandling channelHandling = TagHandling.Name,
            TagHandling roleHandling = TagHandling.Name, TagHandling everyoneHandling = TagHandling.Ignore, TagHandling emojiHandling = TagHandling.Name)
            => MentionUtils.Resolve(this, startIndex, userHandling, channelHandling, roleHandling, everyoneHandling, emojiHandling);
        /// <inheritdoc />
        public string Resolve(TagHandling userHandling = TagHandling.Name, TagHandling channelHandling = TagHandling.Name,
            TagHandling roleHandling = TagHandling.Name, TagHandling everyoneHandling = TagHandling.Ignore, TagHandling emojiHandling = TagHandling.Name)
            => MentionUtils.Resolve(this, 0, userHandling, channelHandling, roleHandling, everyoneHandling, emojiHandling);

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">This operation may only be called on a <see cref="RestNewsChannel"/> channel.</exception>
        public async Task CrosspostAsync(RequestOptions options = null)
        {
            if (!(Channel is INewsChannel))
            {
                throw new InvalidOperationException("Publishing (crossposting) is only valid in news channels.");
            }

            await MessageHelper.CrosspostAsync(this, Discord, options);
        }

        private string DebuggerDisplay => $"{Author}: {Content} ({Id}{(Attachments.Count > 0 ? $", {Attachments.Count} Attachments" : "")})";
    }
}
