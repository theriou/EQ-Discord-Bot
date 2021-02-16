using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class Help : BaseCommandModule
    {
        [Command("help")]
        public async Task EQRPatch(CommandContext ctx)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Help Requested, Sending Message"); Console.ResetColor();

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Description = "The following Commands are supported (will not respond in these DM's):\n"
                    + "!achieve <name> - Can use !achievet for Test, !achieveb for Beta\n"
                    + "!event or !eventu - Lists Current Events or Upcoming Events\n"
                    + "!faction <name> - Can use !factiont for Test, !factionb for Beta\n"
                    + "!item <name>\n"
                    //+ "!overa <name> Agent Name - can use !overat for Test, !overab for Beta\n"
                    //+ "!overq <name> Quest Name - can use !overqt for Test, !overqb for Beta\n"
                    + "!patch\n"
                    + "!spell <name> - Can use !spellt for Test, !spellb for Beta\n"
                    //+ "!ts <name> tradeskill search\n"
                };

                await ctx.Member.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }
    }
}
