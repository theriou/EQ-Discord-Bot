using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class ItemSearch : BaseCommandModule
    {
        [Command("item")]
        public async Task ItemCommand(CommandContext ctx, [RemainingText]string itemSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string itemReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Item Data Requested", "Cyan");

                if (string.IsNullOrEmpty(itemSearch))
                {
                    Globals.CWLMethod("Item Searched without Name", "Red");
                    itemReturn = "Make sure to enter an Item to search for after the command";
                }
                else
                {
                    Globals.CWLMethod($"Searched for Item: {itemSearch}", "Cyan");
                    itemReturn = GlobalResults.GlobalResult(itemSearch, "item");
                }

                Globals.CWLMethod("Sending Item Message...", "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Gold,
                    Description = itemReturn
                };

                await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            }
        }
    }
}
