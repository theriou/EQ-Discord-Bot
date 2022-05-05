using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class EQEvent : BaseCommandModule
    {
        [Command("event"), Aliases("eventu")]
        public async Task EventCommand(CommandContext ctx)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string getEventCommand = ctx.Message.ToString(), 
                    eventDataReturn = string.Empty,
                    eventType = string.Empty;
                bool eventUpcoming = getEventCommand.IndexOf(ctx.Prefix + "eventu", 0, StringComparison.CurrentCultureIgnoreCase) >= 0;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Event Data Requested", "Cyan");

                if (Globals.eqEvent == null)
                {
                    Globals.CWLMethod("Event Failed...", "Red");
                    eventDataReturn = "Event Failed! Retry Later.";
                }
                else
                {
                    if (eventUpcoming)
                    {
                        eventType = "Upcoming";
                        eventDataReturn = GlobalResults.GlobalResult("upcoming", "event");
                    }
                    else
                    {
                        eventType = "Current";
                        eventDataReturn = GlobalResults.GlobalResult("", "event");
                    }

                    Globals.CWLMethod($"Event Success, getting {eventType} Data...", "Cyan");
                }

                Globals.CWLMethod("Sending Event Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Wheat,
                    Description = eventDataReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
