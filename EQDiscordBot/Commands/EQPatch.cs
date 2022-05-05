using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class EQPatch : BaseCommandModule
    {
        [Command("patch")]
        public async Task PatchCommand(CommandContext ctx)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string patchReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Patch Data Requested", "Cyan");

                if (Globals.patchData == null)
                {
                    Globals.CWLMethod("Patch Failed...", "Red");
                    patchReturn = "Patch Failed! Retry Later.";
                }
                else
                {
                    Globals.CWLMethod("Patch Success, getting Data...", "Cyan");
                    patchReturn = GlobalResults.GlobalResult("", "patch");
                }

                Globals.CWLMethod("Sending Patch Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.CornflowerBlue,
                    Description = patchReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
