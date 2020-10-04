using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class SpellSearch : BaseCommandModule
    {
        [Command("spell"), Aliases("spellt", "spellb")]
        public async Task Spell(CommandContext ctx, [RemainingText]string spellSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id))
            {
                string spellSource = string.Empty,
                    spellReturn = string.Empty,
                    spellDBSource= string.Empty;

                await ctx.TriggerTypingAsync();

                string getSpellSource = ctx.Message.ToString();
                bool spellTest = getSpellSource.Contains(ctx.Prefix + "spellt"),
                    spellBeta = getSpellSource.Contains(ctx.Prefix + "spellb");

                if (spellTest == true)
                {
                    spellReturn = GlobalResults.GlobalResult(spellSearch, "spellt");
                    spellDBSource = "Test";
                }
                else if (spellBeta == true)
                {
                    spellReturn = GlobalResults.GlobalResult(spellSearch, "spellb");
                    spellDBSource = "Beta";
                }
                else
                {
                    spellReturn = GlobalResults.GlobalResult(spellSearch, "spell");
                    spellDBSource = "Live";
                }
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Searched for Spell: " + spellSearch + " Source: " + spellDBSource); Console.ResetColor();

                if (string.IsNullOrEmpty(spellReturn) || ctx.Member.IsBot)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Spell Search Null or Bot"); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = "No Results found. Try something else!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Spell Success, Sending Message..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = spellReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
            }
        }
    }
}
