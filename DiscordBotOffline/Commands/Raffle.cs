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
        public static int raffleTotal = 0;
        public static Dictionary<string, string> raffleList = new Dictionary<string, string>();

        [Command("raffle"), Aliases("raffles", "rafflee", "raffler")]
        public async Task Raffles(CommandContext ctx, [RemainingText]int raffleNumber)
        {
            if (Globals.raffleChannelsAllowed.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                string getRaffle = ctx.Message.ToString(),
                        raffleWinners = string.Empty;
                bool raffleDrawing = getRaffle.Contains(ctx.Prefix + "raffler"),
                    raffleEnd = getRaffle.Contains(ctx.Prefix + "rafflee"),
                    raffleStart = getRaffle.Contains(ctx.Prefix + "raffles");


                if (raffleStart && Globals.raffleChannelsAdmins.Contains(ctx.User.Id))
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Member {ctx.Member.Username} Started a Raffle"); Console.ResetColor();
                    raffleStatus = true;
                    raffleDraw = false;
                    raffleTotal = 0;
                    raffleList.Clear();

                    await ctx.TriggerTypingAsync();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Description = "Starting up a Raffle, use !raffle to enter!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }

                if (!raffleStart && !raffleDrawing && !raffleEnd && raffleStatus)
                {
                    bool key = raffleList.Any(tr => tr.Value.Equals(ctx.Member.Mention, StringComparison.CurrentCultureIgnoreCase));
                    if (key)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"Member {ctx.Member.Username} - {ctx.Member.Mention} Dupe Entry"); Console.ResetColor();
                        await ctx.Message.DeleteAsync("Dupe Entry!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"Added {ctx.Member.Username} - {ctx.Member.Mention}"); Console.ResetColor();
                        raffleList.Add(ctx.Member.Username, ctx.Member.Mention);
                        await ctx.Message.DeleteAsync("Entered!");
                    }
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Current Total Entries: {raffleList.Count}"); Console.ResetColor();
                }
                
                if (!raffleStatus && !raffleDraw)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"{ctx.Member.Username} tried to Raffle with none Active"); Console.ResetColor();

                    await ctx.TriggerTypingAsync();

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Description = "Sorry, there is not an Active Raffle."
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }

                if (raffleDrawing && Globals.raffleChannelsAdmins.Contains(ctx.User.Id) && raffleDraw)
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Checking for Total Entries"); Console.ResetColor();

                    await ctx.TriggerTypingAsync();

                    if (raffleTotal == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("No Entries"); Console.ResetColor();

                        var embed = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Description = "There were no Entries to Draw..."
                        };

                        await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Picking Winners from {raffleTotal} entries"); Console.ResetColor();

                        if (raffleNumber < 1)
                        {
                            raffleNumber = 1;
                        }
                        else if (raffleNumber > 10)
                        {
                            raffleNumber = 10;
                        }
                        raffleWinners += $"Now Drawing for {raffleNumber} Winners...\n\n";


                        Dictionary<string, string> dict = raffleList;
                        foreach (object mentionName in RandomValues(dict).Take(raffleNumber))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"{mentionName} Won"); Console.ResetColor();
                            raffleWinners += $"Winner {mentionName}\n";
                        }

                        var embed = new DiscordEmbedBuilder
                        {
                            Color = DiscordColor.Green,
                            Description = raffleWinners
                        };

                        await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                    }
                }

                if (raffleEnd && Globals.raffleChannelsAdmins.Contains(ctx.User.Id))
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Member {ctx.Member.Username} Ended a Raffle"); Console.ResetColor();

                    raffleStatus = false;
                    raffleDraw = true;

                    await ctx.TriggerTypingAsync();

                    raffleTotal = raffleList.Count;

                    var embed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Description = $"The Raffle has Ended with {raffleTotal} Entries... Prepare for the Reward Drawings!"
                    };

                    await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
                }

            }

            }
        }
    }
