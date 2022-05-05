using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class AchievementSearch : BaseCommandModule
    {
        [Command("achieve"), Aliases("achievet", "achieveb")]
        public async Task AchievementCommand(CommandContext ctx, [RemainingText]string achieveSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string achieveReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Achievement Data Requested", "Cyan");

                if (string.IsNullOrEmpty(achieveSearch))
                {
                    Globals.CWLMethod("Achievement Searched without Name", "Red");
                    achieveReturn = "Make sure to enter an Achievement to search for after the command";
                }
                else
                {
                    string getAchieveSource = ctx.Message.ToString(),
                        achieveDBSource = string.Empty;
                    bool achieveTest = getAchieveSource.IndexOf(ctx.Prefix + "achievet", 0, StringComparison.CurrentCultureIgnoreCase) >= 0,
                        achieveBeta = getAchieveSource.IndexOf(ctx.Prefix + "achieveb", 0, StringComparison.CurrentCultureIgnoreCase) >= 0;

                    if (achieveBeta == true && Globals.achieveBetaName.Count > 1)
                    {
                        achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achieveb");
                        achieveDBSource = "Beta";
                    }
                    else if (achieveTest == true && Globals.achieveTestName.Count > 1)
                    {
                        achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achievet");
                        achieveDBSource = "Test";
                    }
                    else
                    {
                        achieveReturn = GlobalResults.GlobalResult(achieveSearch, "achieve");
                        achieveDBSource = "Live";
                    }

                    Globals.CWLMethod($"Searched for Achievement: {achieveSearch} Source: {achieveDBSource}", "Cyan");
                }

                Globals.CWLMethod("Sending Achievement Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Gold,
                    Description = achieveReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
