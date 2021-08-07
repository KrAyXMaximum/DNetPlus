using Discord;
using Discord.Commands;
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
        public async Task Create()
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

        [Command("test"), RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task Ahh(string user = "<@!622890595614195722>")
        {
            IGuildUser User = null;
            if (!string.IsNullOrEmpty(user))
            {
                ulong UID = MentionToID(user);
                //if (UID == 0)
                //    ThrowError.Log("Invalid user mention/id.");
                User = (Context.Channel as MockedThreadChannel).GetUser(UID) as IGuildUser ?? await Context.Client.Rest.GetGuildUserAsync(Context.Guild.Id, UID);
            }
            else
                User = Context.User as SocketGuildUser;

            //if (User == null)
            //    ThrowError.Log($"Could not find user in server.");

            string Joined = "Unknown";
            List<string> Values = new List<string>();
            string Mention = $"{User.ToString()} - <@{User.Id}>";
            Values.Add($"**Created:** `{User.CreatedAt.Day} {User.CreatedAt.DateTime.ToString("MMMM")} {User.CreatedAt.Year}`");
            if (User.JoinedAt.HasValue)
            {
                Joined = $"{User.JoinedAt.Value.Day} {User.JoinedAt.Value.DateTime.ToString("MMMM")} {User.JoinedAt.Value.Year}";
            }
            if (User.IsBot)
                Mention += " <:dboatsBotTag:500353491169181711>";

            if (User.Id == 190590364871032834)
                Joined = "18 September 2018";


            Values.Add($"**Joined:** `{Joined}`");

            List<string> Badges = new List<string>();
            IGuildUser GU = User as IGuildUser;

            if (GU.RoleIds.Contains<ulong>(491309023908593666))
                Badges.Add("<:dboatsWumpusWizard:500353334016999435> Admin");
            else if (GU.RoleIds.Contains<ulong>(588781632958234657))
                Badges.Add("<a:dboatsMeowbadass:500353187308634122> Management");
            else if (GU.RoleIds.Contains<ulong>(439872278809935873))
                Badges.Add(":tools: Web Mod");
            else if (GU.RoleIds.Contains<ulong>(439872254390829077))
                Badges.Add(":hammer_pick: Mod");
            else if (GU.RoleIds.Contains<ulong>(449253443513876480))
                Badges.Add(":hammer: Trial Mod");

            if (GU.RoleIds.Contains<ulong>(533692716702367785))
                Badges.Add("<:dboatsSadCat:586284333342654478> Former Staff");

            if (GU.RoleIds.Contains<ulong>(555125868893175935))
                Badges.Add("<:dboatsDiscordPartner:500353536236716035> Partner");

            if (GU.RoleIds.Contains<ulong>(524656361590489108))
                Badges.Add("<:dboatsJacobwat:729450990243283004> Premium User");

            if (GU.RoleIds.Contains<ulong>(439872287215190017))
                Badges.Add("<:dboatsBoat:535107904714440704> Certified Bot Dev");
            else if (GU.RoleIds.Contains<ulong>(439872291044589570))
                Badges.Add("<:dboatsBoat:535107904714440704> Bot Dev");

            if (GU.RoleIds.Contains<ulong>(585547797680488463))
                Badges.Add("<:dboatsDiscordNitro:500353535901433866> Nitro Booster");

            EmbedBuilder embed = new EmbedBuilder()
            {
                ThumbnailUrl = User.GetAvatarUrl(ImageFormat.WebP, 256) ?? User.GetDefaultAvatarUrl(),
                Description = Mention
            };

            if (Badges.Count != 0)
                embed.Description += $"\n\n{string.Join(" | ", Badges)}";


            embed.Description += $"\n\n{string.Join("\n", Values)}";

            await Context.Channel.SendMessageAsync($"**ID:** {User.Id}", embed: embed.Build());
        }

        [Command("usertags")]
        public async Task UserTags()
        {
            Console.Write(Context.User.PublicFlags.Value);
            await ReplyAsync("Done");
        }
    }
}
