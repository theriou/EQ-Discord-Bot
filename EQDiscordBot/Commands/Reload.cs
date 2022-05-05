using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class Reload : BaseCommandModule
    {
        [Command("reload")]
        public async Task ReloadCommand(CommandContext ctx, [RemainingText]string reloadType)
        {
            if (Globals.channelsAllowedAdmin.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                Globals.CWLMethod("Attempting to Reload", "Cyan");

                string reloadSection = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Reload Command Used", "Cyan");

                switch (reloadType.ToLower())
                {
                    case "achieve":
                        Globals.ReloadValues("achieve");
                        reloadSection = "Achievement";
                        break;
                    case "channel":
                        Globals.ReloadValues("channel");
                        reloadSection = "Channel";
                        break;
                    case "dbstr":
                        Globals.ReloadValues("dbstr");
                        reloadSection = "Faction and Overseer";
                        break;
                    case "event":
                        Globals.ReloadValues("events");
                        reloadSection = "Event";
                        break;
                    case "item":
                        Globals.ReloadValues("item");
                        reloadSection = "Item";
                        break;
                    case "patch":
                        Globals.ReloadValues("patch");
                        reloadSection = "Patch";
                        break;
                    case "spell":
                        Globals.ReloadValues("spell");
                        reloadSection = "Spell";
                        break;
                    case "all":
                        Globals.ReloadValues("achieve");
                        Globals.ReloadValues("channel");
                        Globals.ReloadValues("dbstr");
                        Globals.ReloadValues("events");
                        Globals.ReloadValues("item");
                        Globals.ReloadValues("patch");
                        Globals.ReloadValues("spell");
                        reloadSection = "All File";
                        break;
                }

                reloadSection += " Reload should be Completed!";

                Globals.CWLMethod(reloadSection, "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Description = reloadSection
                };

                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }
    }
}
