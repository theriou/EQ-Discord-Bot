using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class EQPatch : BaseCommandModule
    {
        [Command("patch")]
        public async Task EQRPatch(CommandContext ctx)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                await ctx.TriggerTypingAsync();

                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Getting Patch Data"); Console.ResetColor();

                string patchReturn = GlobalResults.GlobalResult("", "patch");

                if (string.IsNullOrEmpty(patchReturn))
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Patch Failed or a Bot Requested"); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.CornflowerBlue,
                        Description = "Patch Failed! Retry Later."
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Patch Success, Sending Message..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.CornflowerBlue,
                        Description = patchReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
            }
        }
    }
}
