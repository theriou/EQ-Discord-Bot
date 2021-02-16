using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class EQEvent : BaseCommandModule
    {
        [Command("event"), Aliases("eventu")]
        public async Task EQEvents(CommandContext ctx)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                await ctx.TriggerTypingAsync();

                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Getting Event Data"); Console.ResetColor();

                string getEventCommand = ctx.Message.ToString(), 
                    eventDataReturn = string.Empty;
                bool eventUpcoming = getEventCommand.Contains(ctx.Prefix + "eventu");


                if (eventUpcoming)
                {
                    eventDataReturn = GlobalResults.GlobalResult("upcoming", "event");
                }
                else
                {
                    eventDataReturn = GlobalResults.GlobalResult("", "event");
                }

                if (Globals.eqrEvent == null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Event Failed..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Wheat,
                        Description = "Event Failed! Retry Later."
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Event Success, Sending Message..."); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Wheat,
                        Description = eventDataReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
            }
        }
    }
}
