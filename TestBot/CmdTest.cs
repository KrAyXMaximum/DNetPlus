using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DNetPlus_InteractiveButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBot
{
    public class CmdTest : ModuleBase<SocketCommandContext>
    {
        private InteractiveButtonsService ib { get; set; }
        public CmdTest(InteractiveButtonsService inter)
        {
            ib = inter;
        }
        [Command("button", RunMode = RunMode.Async)]
        public async Task Button()
        {
            IUserMessage Mes = await ReplyAsync("Select a fruit", components: new InteractionRow[]
            {
                 new InteractionRow
                 {
                     Buttons = new InteractionButton[]
                     {
                         new InteractionButton(ComponentButtonType.Primary, "Apple", "apple"),
                          new InteractionButton(ComponentButtonType.Primary, "Orange", "orange"),
                     }
                 }
            });
            InteractionData Reply = await ib.NextButtonAsync(Context, Mes, true, new TimeSpan(0, 5, 0));
            if (Reply == null)
            {
                await ReplyAsync("Invalid data");
                return;
            }
            await Context.Channel.SendInteractionMessageAsync(Reply, $"{Context.User.Mention} you selected {Reply.CustomId}");
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

        [Command("test")]
        public async Task Ahh(string ani = "")
        {
            await ReplyInteractionAsync("Hello");

        }

        [Command("usertags")]
        public async Task UserTags()
        {
            Console.Write(Context.User.PublicFlags.Value);
            await ReplyAsync("Done");
        }
    }
}
