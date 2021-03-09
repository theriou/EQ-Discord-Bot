using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotOffline.Commands
{
    class Raffle : BaseCommandModule
    {
        public IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            Random rand = new Random();
            List<TValue> values = Enumerable.ToList(dict.Values);
            int size = dict.Count;
            while (true)
            {
                yield return values[rand.Next(size)];
            }
        }

        public static bool raffleStatus = false;
        public static bool raffleDraw = false;
        public static Dictionary<string, string> raffleList = new Dictionary<string, string>();

        [Command("raffle"), Aliases("raffles", "rafflee", "raffler")]
        public async Task RaffleCommand(CommandContext ctx, [RemainingText]int raffleNumber)
        {
            if (Globals.raffleChannelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string getRaffle = ctx.Message.ToString(),
                    raffleReturn = string.Empty;
                bool raffleDrawing = getRaffle.Contains(ctx.Prefix + "raffler"),
                    raffleEnd = getRaffle.Contains(ctx.Prefix + "rafflee"),
                    raffleStart = getRaffle.Contains(ctx.Prefix + "raffles");

                Globals.CWLMethod("Raffle Command Used", "Cyan");

                if (raffleStart && Globals.raffleChannelsAdmins.Contains(ctx.User.Id))
                {
                    raffleStatus = true;
                    raffleDraw = false;
                    raffleList.Clear();

                    Globals.CWLMethod($"{ctx.Member.Username} Started a Raffle", "Cyan");
                    raffleReturn = $"Starting up a Raffle, use {ctx.Prefix}raffle to enter!";
                }

                if (!raffleStart && !raffleDrawing && !raffleEnd && raffleStatus)
                {
                    bool key = raffleList.Any(tr => tr.Value.Equals(ctx.Member.Mention, StringComparison.CurrentCultureIgnoreCase));
                    if (key)
                    {
                        Globals.CWLMethod($"Member {ctx.Member.Username} - {ctx.Member.Mention} Dupe Entry", "Red");
                        await ctx.Message.DeleteAsync("Dupe Entry!");
                    }
                    else
                    {
                        Globals.CWLMethod($"Added {ctx.Member.Username} - {ctx.Member.Mention}", "Green");
                        raffleList.Add(ctx.Member.Username, ctx.Member.Mention);
                        await ctx.Message.DeleteAsync("Entered!");
                    }
                    Globals.CWLMethod($"Current Total Entries: {raffleList.Count}", "Cyan");
                }

                if (!raffleStatus && !raffleDraw)
                {
                    Globals.CWLMethod($"{ctx.Member.Username} tried to Raffle with none Active", "Red");
                    raffleReturn = "Sorry, there is not an Active Raffle.";
                }

                if (raffleDrawing && Globals.raffleChannelsAdmins.Contains(ctx.User.Id) && raffleDraw)
                {
                    Globals.CWLMethod("Checking for Total Entries", "Cyan");

                    if (raffleList.Count == 0)
                    {
                        Globals.CWLMethod("No Entries", "Cyan");
                        raffleReturn = "There are no Entries to Draw...";
                    }
                    else
                    {
                        Globals.CWLMethod($"Picking Winners from {raffleList.Count} entries", "Cyan");

                        if (raffleNumber < 1)
                        {
                            raffleNumber = 1;
                        }
                        else if (raffleNumber > 10)
                        {
                            raffleNumber = 10;
                        }
                        raffleReturn += $"Now Drawing for {raffleNumber} Winners...\n\n";

                        Dictionary<string, string> dict = raffleList;
                        foreach (object mentionName in RandomValues(dict).Take(raffleNumber))
                        {
                            Globals.CWLMethod($"{mentionName} Won", "Green");
                            raffleReturn += $"Winner {mentionName}\n";
                        }
                    }
                }

                if (raffleEnd && Globals.raffleChannelsAdmins.Contains(ctx.User.Id))
                {
                    raffleStatus = false;
                    raffleDraw = true;

                    Globals.CWLMethod($"{ctx.Member.Username} Ended a Raffle", "Cyan");
                    raffleReturn = $"The Raffle has Ended with {raffleList.Count} Entries... Prepare for the Drawing!";
                }

                if (string.IsNullOrEmpty(raffleReturn))
                {
                }
                else
                {
                    await ctx.TriggerTypingAsync();

                    Globals.CWLMethod("Sending Raffle Message...", "Cyan");

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Description = raffleReturn
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }

            }

        }
    }
}
