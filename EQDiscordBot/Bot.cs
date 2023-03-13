using EQDiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EQDiscordBot
{

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;
            string loadBotFiles = Globals.loadBotFiles;

            string jsonConfig = "config/config.json";

            if (File.Exists(jsonConfig))
            {
                Globals.CWLMethod("Config File Found, starting Bot...", "Yellow");

                using (var fs = File.OpenRead(jsonConfig))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync();

                var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

                var config = new DiscordConfiguration
                {
                    Token = configJson.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                    Intents = DiscordIntents.All
                };

                Client = new DiscordClient(config);
                Client.Ready += (OnClientReady);

                //function for specific reaction changes
                Client.MessageReactionAdded += OnReactionAdded;

                //Set Timer, interval is milliseconds, 1000 = 1
                //process this in Timer_Elapsed function
                Timer timer = new Timer
                {
                    Interval = 180000
                };
                timer.Elapsed += Timer_ElapsedAsync;
                timer.Start();

                var commandsConfig = new CommandsNextConfiguration
                {
                    StringPrefixes = new string[] { configJson.Prefix },
                    EnableDms = false,
                    EnableMentionPrefix = true,
                    CaseSensitive = false,
                    EnableDefaultHelp = false
                };

                Commands = Client.UseCommandsNext(commandsConfig);

                Commands.RegisterCommands<AchievementSearch>();
                Commands.RegisterCommands<EQPatch>();
                Commands.RegisterCommands<EQEvent>();
                Commands.RegisterCommands<FactionSearch>();
                Commands.RegisterCommands<Help>();
                Commands.RegisterCommands<ItemSearch>();
                //Commands.RegisterCommands<OverseerSearch>();
                Commands.RegisterCommands<Raffle>();
                Commands.RegisterCommands<Reload>();
                Commands.RegisterCommands<SpellSearch>();

                var gamePlaying = new DiscordActivity
                {
                    Name = "EverQuest",
                };

                await Client.ConnectAsync(gamePlaying);
                await Task.Delay(-1);
            }
            else
            {
                Globals.CWLMethod("Config File was not found, Bot can't run...", "Red");
                await Task.Delay(-1);
            }
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            var memberRole = e.Guild.GetRole(0);
            if (Globals.roleMessagesAllowed.Contains(e.Message.Id))
            {
                if (Globals.rolesName.ContainsKey(e.Emoji.Name))
                {
                    memberRole = e.Guild.GetRole(Globals.rolesName[e.Emoji.Name]);
                }
                else
                {
                    memberRole = null;
                }

                var member = await e.Guild.GetMemberAsync(e.User.Id);

                if (memberRole == null)
                {
                }
                else
                {
                    var hasMemberRole = member.Roles.FirstOrDefault(s => s == memberRole);

                    if (hasMemberRole == null)
                    {
                        await member.GrantRoleAsync(role: memberRole);
                        Globals.CWLMethod($"{member.DisplayName} added Role: {memberRole.Name}", "Green");
                    }
                    else
                    {
                        await member.RevokeRoleAsync(role: memberRole);
                        Globals.CWLMethod($"{member.DisplayName} removed Role: {memberRole.Name}", "Green");
                    }
                }

                await e.Message.DeleteReactionAsync(e.Emoji, e.User, "auto remove reaction");
            }
        }

        public async Task GetServerStatusData()
        {
            string newStatusResults = string.Empty,
                oldStatusResults = string.Empty,
                messageUpdate = string.Empty;
            var eqResult = "0";

            if (string.IsNullOrEmpty(Globals.censusURL))
            {
                eqResult = null;
            }
            else
            {
                eqResult = await Globals.StatusClient.GetStringAsync(Globals.censusURL);
            }

            if (string.IsNullOrEmpty(eqResult) || (!eqResult.StartsWith("{") && !eqResult.EndsWith("}")))
            {
                Globals.CWLMethod($"Null or Bad Census Data Received, Skipping...", "Red");
            }
            else
            {
                JObject eqStatusResult = JObject.Parse(eqResult);

                foreach (var statusResults in Globals.serverStatus)
                {
                    newStatusResults = eqStatusResult["eq"][statusResults.ServerRegion][statusResults.ServerName]["status"].ToString();
                    oldStatusResults = statusResults.ServerStatus;

                    if (((oldStatusResults == "high" || oldStatusResults == "medium" || oldStatusResults == "low") && (newStatusResults == "locked" || newStatusResults == "down")) ||
                            ((oldStatusResults == "locked" || oldStatusResults == "down") && (newStatusResults == "high" || newStatusResults == "medium" || newStatusResults == "low")))
                    {
                        messageUpdate += $"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}] <@&{statusResults.RolesID}> From {oldStatusResults} to {newStatusResults}\n";
                        Globals.CWLMethod($"{statusResults.ServerName} From {oldStatusResults} to {newStatusResults}", "Green");
                    }

                    statusResults.ServerStatus = newStatusResults;
                }

                Globals.CWLMethod($"Server Status Updated", "Cyan");

                if (string.IsNullOrEmpty(messageUpdate))
                {
                }
                else
                {
                    DiscordChannel channel = await Client.GetChannelAsync(Globals.messageID);
                    await channel.SendMessageAsync(messageUpdate);
                }
            }

        }

        private async void Timer_ElapsedAsync(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Globals.messageID == 0)
                {
                }
                else
                {
                    await GetServerStatusData();
                }
            }
            catch
            {
                Globals.CWLMethod($"Server Status Failed, Skipping this Update...", "Red");
            }
        }

    }
}
