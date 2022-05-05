using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class FactionSearch : BaseCommandModule
    {
        [Command("faction"), Aliases("factiont", "factionb")]
        public async Task FactionCommand(CommandContext ctx, [RemainingText]string factionSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string factionReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Faction Data Requested", "Cyan");

                if (string.IsNullOrEmpty(factionSearch))
                {
                    Globals.CWLMethod("Faction Searched without Name", "Red");
                    factionReturn = "Make sure to enter a Faction to search for after the command";
                }
                else
                {
                    string getFactionSource = ctx.Message.ToString(),
                        factionDBSource = string.Empty;
                    bool factionTest = getFactionSource.IndexOf(ctx.Prefix + "factiont", 0, StringComparison.CurrentCultureIgnoreCase) >= 0,
                        factionBeta = getFactionSource.IndexOf(ctx.Prefix + "factionb", 0, StringComparison.CurrentCultureIgnoreCase) >= 0;

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

                    Globals.CWLMethod($"Searched for Faction: {factionSearch} Source: {factionDBSource}", "Cyan");
                }

                Globals.CWLMethod("Sending Faction Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Gold,
                    Description = factionReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
