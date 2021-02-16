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
        [Command("reloada"), Aliases("reloadc", "reloadd", "reloade", "reloadi", "reloadp", "reloads")]
        public async Task EQRPatch(CommandContext ctx)
        {
            if (Globals.channelsAllowedAdmin.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Attempting to Reload"); Console.ResetColor();

                string getReloadType = ctx.Message.ToString(),
                    reloadSection = string.Empty;
                bool reloadAchieve = getReloadType.Contains(ctx.Prefix + "reloada"),
                    reloadChannels = getReloadType.Contains(ctx.Prefix + "reloadc"),
                    reloadDbStr = getReloadType.Contains(ctx.Prefix + "reloadd"),
                    reloadEvents = getReloadType.Contains(ctx.Prefix + "reloade"),
                    reloadItems = getReloadType.Contains(ctx.Prefix + "reloadi"),
                    reloadPatch = getReloadType.Contains(ctx.Prefix + "reloadp"),
                    reloadSpells = getReloadType.Contains(ctx.Prefix + "reloads");

                await ctx.TriggerTypingAsync();

                if (reloadAchieve == true)
                {
                    Globals.ReloadValues("achieve");
                    reloadSection = "Achievement";
                }
                else if (reloadChannels == true)
                {
                    Globals.ReloadValues("channel");
                    reloadSection = "Channel";
                }
                else if (reloadDbStr == true)
                {
                    Globals.ReloadValues("dbstr");
                    reloadSection = "Faction and Overseer";
                }
                else if (reloadEvents == true)
                {
                    Globals.ReloadValues("events");
                    reloadSection = "Event";
                }
                else if (reloadItems == true)
                {
                    Globals.ReloadValues("item");
                    reloadSection = "Item";
                }
                else if (reloadPatch == true)
                {
                    Globals.ReloadValues("patch");
                    reloadSection = "Patch";
                }
                else if (reloadSpells == true)
                {
                    Globals.ReloadValues("spell");
                    reloadSection = "Spell";
                }

                reloadSection += " Reload should be Completed!";

                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(reloadSection); Console.ResetColor();

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
