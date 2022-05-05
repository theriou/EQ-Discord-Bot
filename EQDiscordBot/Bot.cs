using DiscordBotOffline.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotOffline
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;
            string loadBotFiles = Globals.loadBotFiles;

            string jsonConfig = @"config.json";
            bool configExists = File.Exists(jsonConfig);

            if (configExists)
            {
                Globals.CWLMethod("Config File Found", "Yellow");

                using (var fs = File.OpenRead(jsonConfig))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);

                var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

                var config = new DiscordConfiguration
                {
                    Token = configJson.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
                };

                Client = new DiscordClient(config);
                Client.Ready += (OnClientReady);

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
    }
}
