using Discord;
using Discord.Commands;
using Discord.Net.Converters;
using Discord.Rest;
using Discord.WebSocket;
using DNetPlus_InteractiveButtons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TestBot
{
    [RequireOwner]
    public class CmdTest : ModuleBase<SocketCommandContext>
    {
        private InteractiveButtonsService ib { get; set; }
        public CmdTest(InteractiveButtonsService inter)
        {
            ib = inter;
        }

        [Command("edit")]
        public async Task Edit(string text = "")
        {
            var Msg = await ReplyAsync("Test <@190590364871032834>", allowedMentions: new AllowedMentions());
            await Task.Delay(10000);
            await Msg.ModifyAsync(x => x.Content = "T <@190590364871032834>");
        }

        [Command("emtest")]
        public async Task EmTest()
        {
            await ReplyAsync("Test", components: new InteractionRow[]
            {
                new InteractionRow
                {
                    Buttons = new InteractionButton[]
                    {
                        new InteractionButton(ComponentButtonType.Primary, "Test", "test")
                    }
                }
            });
        }
        [Command("create")]
        public async Task Create()
        {
            await Context.Guild.CreateRoleAsync("", null, null, false, new RequestOptions { });
            await ReplyAsync("Done");
        }

        [Command("delchan"), RequireOwner]
        public async Task DelChan()
        {
            if (Context.Channel is MockedThreadChannel thread)
            {
                await thread.DeleteAsync();
            }
        }

        [Command("embed")]
        public async Task Embed([Remainder] string text)
        {
            await Context.Channel.SendMessageAsync("", embed: new Discord.EmbedBuilder
            {
                Description = text,
                Footer = new EmbedFooterBuilder
                {
                    IconUrl = Context.User.GetAvatarUrl(ImageFormat.WebP, 320) ?? Context.User.GetDefaultAvatarUrl(),
                    Text = Context.User.Username
                }
            }.Build());
        }

        [Command("helloa")]
        public async Task TestAttach()
        {
            var Bytes = File.ReadAllBytes("C:/Users/Brandan/Documents/DiscordBots/Waifu/Testwelcome.jpg");
            try
            {
                await Context.Channel.SendInteractionFileAsync(Context.InteractionData, new MemoryStream(Bytes), "Test.jpg", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [Command("getbutton")]
        public async Task GetButton()
        {
            IMessage Message = await Context.Guild.GetTextChannel(275062423159963648).GetMessageAsync(852184325147590696);
            await ReplyAsync($"Buttons: {Message.Components.First().Buttons.Count()}");
        }

        [Command("testbutton")]
        public async Task TestButton()
        {
            InteractionRow[] rows = new InteractionRow[]
            {
                new InteractionRow
                {
                    Buttons = new InteractionButton[]
                    {
                        new InteractionButton(ComponentButtonType.Primary, "Test", "test")
                    }
                },
                new InteractionRow
                {
                    Dropdown = new InteractionDropdown
                    {
                        Options = new InteractionOption[]
                        {
                            new InteractionOption("Option 1", "op1", "Description", new Emoji("🔨")),
                            new InteractionOption("Option 2", "op2", "Description", Context.Guild.Emotes.First(x => x.Id == 805194713287360632)),
                        }
                    }
                }
            };
            await Context.Channel.SendInteractionMessageAsync(Context.InteractionData, "Test", components: rows);
        }

        [Command("interactive", RunMode = RunMode.Async)]
        public async Task Interactive(string user = "")
        {
            SocketGuildUser GU = Context.User as SocketGuildUser;
            IGuildUser User = Context.Guild.GetUser(ulong.Parse(user)) ?? await Context.Client.Rest.GetGuildUserAsync(Context.Guild.Id, ulong.Parse(user)) as IGuildUser;
            IUserMessage Mes = null;
            try
            {
                EmbedBuilder embed = new EmbedBuilder()
                {
                    Title = "Test",
                    Description = $"**{GU.Nickname ?? GU.Username}** :crossed_swords: **{User.Nickname ?? User.Username}**\n\n" +
                    $"`accept @{Context.User.Username}`"
                };

                Mes = await Context.Channel.SendInteractionMessageAsync(Context.InteractionData, embed: embed.Build(), components: new InteractionRow[]
                {
                    new InteractionRow
                    {
                        Buttons = new InteractionButton[]
                        {
                            new InteractionButton(ComponentButtonType.Secondary, "Accept Invite", "bot-acceptinvite")
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await ReplyAsync("Error");
                return;
            }
            if (Mes == null)
                return;
            InteractionData Reply = await ib.NextButtonAsync(Context, Mes, User, new TimeSpan(0, 1, 0));
            if (Reply == null)
            {
                try
                {
                    await Mes.ModifyAsync(x => x.Content = "Multiplayer invite timed out.");
                }
                catch { }
                return;
            }
            await ReplyAsync("Accepted");
        }

        [Command("addslash")]
        public async Task AddSlash()
        {
            await Context.Guild.CreateCommandAsync(
                new CreateInteraction
                {
                Name = "Hello",
                Description = "Test desc"
                }
            );
        }

        [Command("addslashmulti")]
        public async Task AddSlashMulti(string name, [Remainder] string desc)
        {
            await Context.Guild.CreateCommandsAsync(new CreateInteraction[]
            {
                new CreateInteraction
                {
                Name = name,
                Description = desc
                },
                new CreateInteraction
                {
                Name = name + "2",
                Description = desc + "2"
                },
            });
            await ReplyAsync("Added");
        }

        [Command("button", RunMode = RunMode.Async)]
        public async Task Button()
        {
            await ReplyInteractionAsync("Got inter");
            Console.WriteLine("GOT BUTTON COMMAND");
            try
            {
                Console.WriteLine("BUTTON JSON");

                InteractionRow[] rows = new InteractionRow[]
                {
                 new InteractionRow
                 {
                     Buttons = new InteractionButton[]
                     {
                         new InteractionButton(ComponentButtonType.Primary, "Apple", "apple"),
                          new InteractionButton(ComponentButtonType.Primary, "Orange", "orange"),
                     }
                 }
                };
                IUserMessage Mes = await ReplyAsync("Select a fruit", components: rows);
                InteractionData Reply = await ib.NextButtonAsync(Context, Mes, new EnsureGuildPermissionCriterion(GuildPermission.SendMessages), new TimeSpan(0, 15, 0));
                try
                {
                    foreach (var i in rows.First().Buttons)
                    {
                        i.Disabled = true;
                    }
                    await Mes.ModifyAsync(x => x.Components = rows);
                }
                catch { }
                if (Reply == null)
                {
                    await ReplyAsync("Invalid data");
                    return;
                }
                await Context.Channel.SendInteractionMessageAsync(Reply, $"You chose {Reply.CustomId}", type: Discord.API.Rest.InteractionMessageType.ChannelMessageWithSource);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [Command("rrole")]
        public async Task RRole(string option = "", string role = "")
        {
            await ReplyInteractionAsync("", embed: new EmbedBuilder
            {
                Description = $"{option} | {role}"
            }.Build());
        }

        [Command("sadd")]
        public async Task SAdd()
        {
            await Context.Guild.CreateCommandAsync(new CreateInteraction
            {
                Name = "test",
                Description = "Hello"
            });
            await ReplyAsync("Added");
        }

        [Command("sdel")]
        public async Task Sdel(ulong id)
        {
            await Context.Guild.DeleteCommandAsync(id);
            await ReplyAsync("Deleted");
        }

        [Command("slist")]
        public async Task SList()
        {
            IReadOnlyCollection<RestInteraction> List = await Context.Guild.GetCommandsAsync();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
            {
                Description = string.Join("\n", List.Select(x => $"{x.Name} | `{x.Id}`"))
            }.Build());
        }

        [Command("tget")]
        public async Task Testget()
        {
            try
            {
                System.Collections.Generic.IReadOnlyCollection<Discord.Rest.RestGuildTemplate> Temps = await Context.Guild.GetTemplatesAsync();
                await ReplyAsync(Temps.Count.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [Command("tcreate")]
        public async Task TCreate()
        {
            Discord.Rest.RestGuildTemplate ResGuildTemplate = await Context.Guild.CreateTemplateAsync("Test", "Test", true);
            if (ResGuildTemplate != null)
                await ReplyAsync(ResGuildTemplate.Code);
        }

        [Command("tsync")]
        public async Task Sync(string code)
        {
            await Context.Guild.SyncTemplateAsync(code);
        }

        [Command("tdelete")]
        public async Task Delete(string code)
        {
            await Context.Guild.DeleteTemplateAsync(code);
        }

        [Command("tmodify")]
        public async Task Modify(string code)
        {
            await Context.Guild.ModifyTemplateAsync(code, new Action<TemplateProperties>(x =>
            {
                x.Description = "YOLO";
            }));
        }

        [Command("testemote")]
        public async Task TestEmote([Remainder] string emote)
        {
            Console.WriteLine($"Test - {emote}");
            Emoji e = Emoji.FromUnicode(emote);
            if (e == null)
                await ReplyAsync("Not valid");
            else
                await ReplyAsync("Valid");
        }

        [Command("getsticker")]
        public async Task GetMsg(ulong id)
        {
            Console.WriteLine("Get sticker");
            IMessage Msg = await Context.Channel.GetMessageAsync(id);
            if (Msg == null)
            {
                await ReplyAsync("Invalid message");
                return;
            }

            if (Msg.Stickers.Count() == 0)
            {
                await ReplyAsync("No sticker");
                return;
            }

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Msg.Stickers.First(), Newtonsoft.Json.Formatting.Indented));
        }

        [Command("replyto")]
        public async Task Replyto(ulong id)
        {
            Console.WriteLine("Testing");
            try
            {
                await Context.Channel.SendMessageAsync("Test", reference: new MessageReferenceParams { ChannelId = Context.Channel.Id, MessageId = id });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [Command("embedimage")]
        public async Task EmbedImage(string image)
        {
            await Context.Channel.SendMessageAsync("", embed: new EmbedBuilder
            {
                ImageUrl = image
            }.Build());
        }

        [Command("testowner"), RequireOwner]
        public async Task TestOwner()
        {
            await ReplyAsync("Test owner");
        }

        public ulong MentionToID(string User)
        {
            if (User.StartsWith("("))
                User = User.Replace("(", "");
            if (User.EndsWith(")"))
                User = User.Replace(")", "");
            if (User.StartsWith("<@"))
            {
                User = User.Replace("<@", "").Replace(">", "");
                if (User.Contains("!"))
                    User = User.Replace("!", "");
            }
            try
            {
                return Convert.ToUInt64(User);
            }
            catch { }
            return 0;
        }

        [Command("test")]
        public async Task Test()
        {
            await Context.Channel.SendInteractionMessageAsync(Context.InteractionData, "Loading", type: Discord.API.Rest.InteractionMessageType.AcknowledgeWithSource);
           
            Console.WriteLine("Get members");
            if (!Context.Guild.HasAllMembers)
                await Context.Guild.DownloadUsersAsync();
            Console.WriteLine("Send hello");
            await Context.InteractionData.SendFollowupAsync(Context.Channel, "Hello");
        }

        [Command("usertags")]
        public async Task UserTags()
        {
            Console.Write(Context.User.PublicFlags.Value);
            await ReplyAsync("Done");
        }
    }
}
