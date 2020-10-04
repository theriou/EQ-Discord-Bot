using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class AchievementSearch : BaseCommandModule
    {
        [Command("achieve"), Aliases("achievet", "achieveb")]
        public async Task Spell(CommandContext ctx, [RemainingText]string achieveSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id))
            {
                string achieveSource = string.Empty,
                    achieveReturn = string.Empty,
                    achieveDBSource = string.Empty;

                await ctx.TriggerTypingAsync();

                string getAchieveSource = ctx.Message.ToString();
                bool achieveTest = getAchieveSource.Contains(ctx.Prefix + "achievet"),
                    achieveBeta = getAchieveSource.Contains(ctx.Prefix + "achieveb");

                if (achieveTest == true)
                {
                    achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achievet");
                    achieveDBSource = "Test";
                }
                else if (achieveBeta == true)
                {
                    achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achieveb");
                    achieveDBSource = "Beta";
                }
                else
                {
                    achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achieve");
                    achieveDBSource = "Live";
                }
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Searched for Achievement: " + achieveSearch + " Source: " + achieveDBSource); Console.ResetColor();

                if (string.IsNullOrEmpty(achieveReturn) || ctx.Member.IsBot)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Achievement Search Null or Bot"); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = "No Results found. Try something else!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Achievement Success, Sending Message..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = achieveReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
            }
        }
    }
}
