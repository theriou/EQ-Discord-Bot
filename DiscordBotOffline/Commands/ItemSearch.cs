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
        public async Task Spell(CommandContext ctx, [RemainingText]string itemSearch)
        {
            if (Globals.channelsAllowed.Contains(ctx.Channel.Id))
            {
                string itemReturn = string.Empty;

                await ctx.TriggerTypingAsync();

                itemReturn = GlobalResults.GlobalResult(itemSearch, "item");

                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Searched for Item: " + itemSearch); Console.ResetColor();

                if (string.IsNullOrEmpty(itemReturn) || ctx.Member.IsBot)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Item Search Null or Bot"); Console.ResetColor();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Gold,
                        Description = "No Results found. Try something else!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Item Success, Sending Message..."); Console.ResetColor();

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
}
