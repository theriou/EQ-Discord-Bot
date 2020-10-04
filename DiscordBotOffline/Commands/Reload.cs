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
        [Command("reload"), Aliases("reloads", "reloadd", "reloada", "reloadi", "reloadp")]
        public async Task EQRPatch(CommandContext ctx)
        {
            if (Globals.channelsAllowedAdmin.Contains(ctx.Channel.Id))
            {
                await ctx.TriggerTypingAsync();

                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Attempting to Reload"); Console.ResetColor();

                string getReloadType = ctx.Message.ToString();
                bool reloadSpells = getReloadType.Contains(ctx.Prefix + "reloads"),
                    reloadDbStr = getReloadType.Contains(ctx.Prefix + "reloadd"),
                    reloadAchieve = getReloadType.Contains(ctx.Prefix + "reloada"),
                    reloadItems = getReloadType.Contains(ctx.Prefix + "reloadi"),
                    reloadPatch = getReloadType.Contains(ctx.Prefix + "reloadp");
                string reloadSection = string.Empty;

                if (ctx.Member.IsBot)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Bot Reload Rejected"); Console.ResetColor();
                }
                else
                {
                    if (reloadSpells == true)
                    {
                        Globals.ReloadValues("spell");
                        reloadSection = "Spell Reload";
                    }
                    else if (reloadDbStr == true)
                    {
                        Globals.ReloadValues("dbstr");
                        reloadSection = "Faction and Overseer Reload";
                    }
                    else if (reloadAchieve == true)
                    {
                        Globals.ReloadValues("achieve");
                        reloadSection = "Achievement Reload";
                    }
                    else if (reloadItems == true)
                    {
                        Globals.ReloadValues("item");
                        reloadSection = "Item Reload";
                    }
                    else if (reloadPatch == true)
                    {
                        Globals.ReloadValues("patch");
                        reloadSection = "Patch Reload";
                    }

                    reloadSection += " should be Completed!";

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
}
