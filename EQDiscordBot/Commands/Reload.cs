using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EQDiscordBot.Commands
{
    class Reload : BaseCommandModule
    {
        [Command("reload")]
        public async Task ReloadCommand(CommandContext ctx, [RemainingText]string reloadType)
        {
            if (Globals.channelsAllowedAdmin.Contains(ctx.Channel.Id) && !ctx.Member.IsBot)
            {
                Globals.CWLMethod("Attempting to Reload", "Cyan");

                string reloadSection = string.Empty;
                bool defaultReply = false;

                await ctx.TriggerTypingAsync();

                Globals.CWLMethod("Reload Command Used", "Cyan");

                reloadType = (String.IsNullOrEmpty(reloadType)) ? "unknown" : reloadType;

                switch (reloadType.ToLower())
                {
                    case "achieve":
                        Globals.ReloadValues("achieve");
                        reloadSection = "Achievement";
                        break;
                    case "channel":
                        Globals.ReloadValues("channel");
                        reloadSection = "Channel";
                        break;
                    case "dbstr":
                        Globals.ReloadValues("dbstr");
                        reloadSection = "Faction and Overseer";
                        break;
                    case "event":
                        Globals.ReloadValues("event");
                        reloadSection = "Event";
                        break;
                    case "item":
                        Globals.ReloadValues("item");
                        reloadSection = "Item";
                        break;
                    case "patch":
                        Globals.ReloadValues("patch");
                        reloadSection = "Patch";
                        break;
                    case "role":
                        Globals.ReloadValues("role");
                        reloadSection = "Role and Server Status";
                        break;
                    case "spell":
                        Globals.ReloadValues("spell");
                        reloadSection = "Spell";
                        break;
                    case "all":
                        Globals.ReloadValues("achieve");
                        Globals.ReloadValues("channel");
                        Globals.ReloadValues("dbstr");
                        Globals.ReloadValues("events");
                        Globals.ReloadValues("item");
                        Globals.ReloadValues("patch");
                        Globals.ReloadValues("role");
                        Globals.ReloadValues("spell");
                        reloadSection = "All File";
                        break;
                    default:
                        reloadSection = "A Type of Reload must be Specified after the command: achieve, channel, dbstr, event, item, patch, role, spell or all";
                        defaultReply = true;
                        break;
                }

                reloadSection += (defaultReply == true) ? "" : " Reload should be Completed!";

                Globals.CWLMethod(reloadSection, "Cyan");

                var embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Description = reloadSection
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
