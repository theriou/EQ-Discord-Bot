using System;
using System.Collections.Generic;

namespace DiscordBotOffline
{
    class Globals
    {
        public static string loadBotFiles = string.Empty;
        public static ulong[] channelsAllowedAdmin = LinkBotChannels.AllowedChannels("admin");
        public static ulong[] channelsAllowed = LinkBotChannels.AllowedChannels("normal");
        public static ulong[] raffleChannelsAdmins = LinkBotChannels.AllowedRaffleChannels("admin");
        public static ulong[] raffleChannelsAllowed = LinkBotChannels.AllowedRaffleChannels("channel");

        public static Dictionary<ulong, string> spellBetaName = ParseFiles.ParseFile("spell", "beta");
        public static Dictionary<ulong, string>[] dbStrResultsB = MultiParseFiles.ParseDBStrFiles("beta");
        public static Dictionary<ulong, string> factionBetaName = dbStrResultsB[0];
        public static Dictionary<ulong, string> overseerBetaAgent = dbStrResultsB[1];
        public static Dictionary<ulong, string> overseerBetaQuest = dbStrResultsB[2];
        public static Dictionary<ulong, string> achieveBetaName = ParseFiles.ParseFile("achieve", "beta");

        public static Dictionary<ulong, string> spellLiveName = ParseFiles.ParseFile("spell", "live");
        public static Dictionary<ulong, string>[] dbStrResultsL = MultiParseFiles.ParseDBStrFiles("live");
        public static Dictionary<ulong, string> factionLiveName = dbStrResultsL[0];
        public static Dictionary<ulong, string> overseerLiveAgent = dbStrResultsL[1];
        public static Dictionary<ulong, string> overseerLiveQuest = dbStrResultsL[2];
        public static Dictionary<ulong, string> achieveLiveName = ParseFiles.ParseFile("achieve", "live");

        public static Dictionary<ulong, string> spellTestName = ParseFiles.ParseFile("spell", "test");
        public static Dictionary<ulong, string>[] dbStrResultsT = MultiParseFiles.ParseDBStrFiles("test");
        public static Dictionary<ulong, string> factionTestName = dbStrResultsT[0];
        public static Dictionary<ulong, string> overseerTestAgent = dbStrResultsT[1];
        public static Dictionary<ulong, string> overseerTestQuest = dbStrResultsT[2];
        public static Dictionary<ulong, string> achieveTestName = ParseFiles.ParseFile("achieve", "test");

        public static Dictionary<ulong, string> itemName = ParseFiles.ParseFile("item", "");

        public static string[] patchData = ParseFiles.ParsePatchFile();

        public static List<ParseFiles.EQEvents> eqEvent = ParseFiles.ParseEventFile();

        public static Dictionary<ulong, string> GetResults(string resultType)
        {
            Dictionary<ulong, string> resultOutput = null;

            switch (resultType)
            {
                case "achieve":
                    resultOutput = achieveLiveName;
                    break;
                case "achieveb":
                    resultOutput = achieveBetaName;
                    break;
                case "achievet":
                    resultOutput = achieveTestName;
                    break;
                case "faction":
                    resultOutput = factionLiveName;
                    break;
                case "factionb":
                    resultOutput = factionBetaName;
                    break;
                case "factiont":
                    resultOutput = factionTestName;
                    break;
                case "item":
                    resultOutput = itemName;
                    break;
                case "spell":
                    resultOutput = spellLiveName;
                    break;
                case "spellb":
                    resultOutput = spellBetaName;
                    break;
                case "spellt":
                    resultOutput = spellTestName;
                    break;
            }

            return resultOutput;
        }

        public static string[] GetGlobals(string urlType)
        {
            string dbSource = string.Empty,
                outputUrl = string.Empty,
                sourceType = string.Empty;
            const string dbSourceB = "Beta",
                dbSourceL = "Live",
                dbSourceT = "Test",
                dbUrlSourceB = "&source=beta",
                dbUrlSourceT = "&source=test",
                achieveStart = "https://achievements.eqresource.com/achievements.php?id=",
                eventStart = "https://events.eqresource.com/index.php?action=display_event&oid=",
                factionStart = "https://factions.eqresource.com/factions.php?id=",
                itemStart = "https://items.eqresource.com/items.php?id=",
                spellStart = "https://spells.eqresource.com/spells.php?id=";

            switch (urlType)
            {
                case "achieve":
                    outputUrl = achieveStart;
                    dbSource = dbSourceL;
                    break;
                case "achievet":
                    sourceType = dbUrlSourceT;
                    outputUrl = achieveStart;
                    dbSource = dbSourceT;
                    break;
                case "achieveb":
                    sourceType = dbUrlSourceB;
                    outputUrl = achieveStart;
                    dbSource = dbSourceB;
                    break;
                case "event":
                    outputUrl = eventStart;
                    break;
                case "faction":
                    outputUrl = factionStart;
                    dbSource = dbSourceL;
                    break;
                case "factiont":
                    sourceType = dbUrlSourceT;
                    outputUrl = factionStart;
                    dbSource = dbSourceT;
                    break;
                case "factionb":
                    sourceType = dbUrlSourceB;
                    outputUrl = factionStart;
                    dbSource = dbSourceB;
                    break;
                case "item":
                    outputUrl = itemStart;
                    break;
                case "patch":
                    break;
                case "spell":
                    outputUrl = spellStart;
                    dbSource = dbSourceL;
                    break;
                case "spellt":
                    sourceType = dbUrlSourceT;
                    outputUrl = spellStart;
                    dbSource = dbSourceT;
                    break;
                case "spellb":
                    sourceType = dbUrlSourceB;
                    outputUrl = spellStart;
                    dbSource = dbSourceB;
                    break;
            }

            return new[] { sourceType, outputUrl, dbSource };
        }

        public static void ReloadValues(string reloadType)
        {
            switch (reloadType)
            {
                case "achieve":
                    achieveBetaName = ParseFiles.ParseFile("achieve", "beta");
                    achieveLiveName = ParseFiles.ParseFile("achieve", "live");
                    achieveTestName = ParseFiles.ParseFile("achieve", "test");
                    break;
                case "channel":
                    channelsAllowedAdmin = LinkBotChannels.AllowedChannels("admin");
                    channelsAllowed = LinkBotChannels.AllowedChannels("normal");
                    raffleChannelsAdmins = LinkBotChannels.AllowedRaffleChannels("admin");
                    raffleChannelsAllowed = LinkBotChannels.AllowedRaffleChannels("channel");
                    break;
                case "dbstr":
                    dbStrResultsB = MultiParseFiles.ParseDBStrFiles("beta");
                    factionBetaName = dbStrResultsB[0];
                    overseerBetaAgent = dbStrResultsB[1];
                    overseerBetaQuest = dbStrResultsB[2];
                    dbStrResultsL = MultiParseFiles.ParseDBStrFiles("live");
                    factionLiveName = dbStrResultsL[0];
                    overseerLiveAgent = dbStrResultsL[1];
                    overseerLiveQuest = dbStrResultsL[2];
                    dbStrResultsT = MultiParseFiles.ParseDBStrFiles("test");
                    factionTestName = dbStrResultsT[0];
                    overseerTestAgent = dbStrResultsT[1];
                    overseerTestQuest = dbStrResultsT[2];
                    break;
                case "events":
                    eqEvent = ParseFiles.ParseEventFile();
                    break;
                case "item":
                    itemName = ParseFiles.ParseFile("item", "");
                    break;
                case "patch":
                    patchData = ParseFiles.ParsePatchFile();
                    break;
                case "spell":
                    spellBetaName = ParseFiles.ParseFile("spell", "beta");
                    spellLiveName = ParseFiles.ParseFile("spell", "live");
                    spellTestName = ParseFiles.ParseFile("spell", "test");
                    break;
            }
        }

        public static void CWLMethod(string cwlText, string cwlColor)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cwlColor); Console.WriteLine(cwlText); Console.ResetColor();
        }
    }
}
