using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class SpellSearch : BaseCommandModule
    {
        [Command("spell"), Aliases("spellt", "spellb")]
        public async Task SpellCommand(CommandContext ctx, [RemainingText]string spellSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string spellReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Spell Data Requested", "Cyan");

                if (string.IsNullOrEmpty(spellSearch))
                {
                    Globals.CWLMethod("Spell Searched without Name", "Red");
                    spellReturn = "Make sure to enter a Spell to search for after the command";
                }
                else
                {
                    string getSpellSource = ctx.Message.ToString(),
                        spellDBSource = string.Empty;
                    bool spellTest = getSpellSource.IndexOf(ctx.Prefix + "spellt", 0, StringComparison.CurrentCultureIgnoreCase) >= 0,
                        spellBeta = getSpellSource.IndexOf(ctx.Prefix + "spellb", 0, StringComparison.CurrentCultureIgnoreCase) >= 0;

                    if (spellBeta == true && Globals.spellBetaName.Count > 1)
                    {
                        spellReturn = GlobalResults.GlobalResult(spellSearch, "spellb");
                        spellDBSource = "Beta";
                    }
                    else if (spellTest == true && Globals.spellTestName.Count > 1)
                    {
                        spellReturn = GlobalResults.GlobalResult(spellSearch, "spellt");
                        spellDBSource = "Test";
                    }
                    else
                    {
                        spellReturn = GlobalResults.GlobalResult(spellSearch, "spell");
                        spellDBSource = "Live";
                    }

                    Globals.CWLMethod($"Searched for Spell: {spellSearch} Source: {spellDBSource}", "Cyan");
                }

                Globals.CWLMethod("Sending Spell Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Gold,
                    Description = spellReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
