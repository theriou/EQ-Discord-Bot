using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class FactionSearch : BaseCommandModule
    {
        [Command("faction"), Aliases("factiont", "factionb")]
        public async Task Spell(CommandContext ctx, [RemainingText]string factionSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id))
            {
                string factionSource = string.Empty,
                    factionReturn = string.Empty,
                    factionDBSource = string.Empty;

                await ctx.TriggerTypingAsync();

                string getFactionSource = ctx.Message.ToString();
                bool factionTest = getFactionSource.Contains(ctx.Prefix + "factiont"),
                    factionBeta = getFactionSource.Contains(ctx.Prefix + "factionb");

                if (factionBeta == true && Globals.factionBetaName.Count > 1)
                {
                    factionReturn = GlobalResults.GlobalResult(factionSearch, "factionb");
                    factionDBSource = "Beta";
                }
                else if (factionTest == true && Globals.factionTestName.Count > 1)
                {
                    factionReturn = GlobalResults.GlobalResult(factionSearch, "factiont");
                    factionDBSource = "Test";
                }
                else
                {
                    factionReturn = GlobalResults.GlobalResult(factionSearch, "faction");
                    factionDBSource = "Live";
                }
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Searched for Faction: " + factionSearch + " Source: " + factionDBSource); Console.ResetColor();

                if (string.IsNullOrEmpty(factionReturn) || ctx.Member.IsBot)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Faction Search Null or Bot"); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = "No Results found. Try something else!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Faction Success, Sending Message..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = factionReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
            }
        }
    }
}
