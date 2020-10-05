using System.Collections.Generic;

namespace DiscordBotOffline
{
    class Globals
    {
        public static ulong[] channelsAllowed = LinkBotChannels.AllowedChannels("normal");
        public static ulong[] channelsAllowedAdmin = LinkBotChannels.AllowedChannels("admin");

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

        public static Dictionary<ulong, string> spellBetaName = ParseFiles.ParseFile("spell", "beta");
        public static Dictionary<ulong, string>[] dbStrResultsB = MultiParseFiles.ParseDBStrFiles("beta");
        public static Dictionary<ulong, string> factionBetaName = dbStrResultsB[0];
        public static Dictionary<ulong, string> overseerBetaAgent = dbStrResultsB[1];
        public static Dictionary<ulong, string> overseerBetaQuest = dbStrResultsB[2];
        public static Dictionary<ulong, string> achieveBetaName = ParseFiles.ParseFile("achieve", "beta");

        public static Dictionary<ulong, string> itemName = ParseFiles.ParseFile("item", "");
        public static string[] patchData = ParseFiles.ParsePatchFile();

        public static Dictionary<ulong, string> GetResults(string resultType)
        {
            Dictionary<ulong, string> resultOutput = null;

            switch (resultType)
            {
                case "spell":
                    resultOutput = spellLiveName;
                    break;
                case "spellt":
                    resultOutput = spellTestName;
                    break;
                case "spellb":
                    resultOutput = spellBetaName;
                    break;
                case "faction":
                    resultOutput = factionLiveName;
                    break;
                case "factiont":
                    resultOutput = factionTestName;
                    break;
                case "factionb":
                    resultOutput = factionBetaName;
                    break;
                case "achieve":
                    resultOutput = achieveLiveName;
                    break;
                case "achievet":
                    resultOutput = achieveTestName;
                    break;
                case "achieveb":
                    resultOutput = achieveBetaName;
                    break;
                case "item":
                    resultOutput = itemName;
                    break;
            }

            return resultOutput;
        }

        public static string[] GetGlobals(string urlType)
        {
            string sourceType = string.Empty,
            outputUrl = string.Empty,
            dbSource = string.Empty;
            const string dbSourceL = "Live",
            dbSourceB = "Beta",
            dbSourceT = "Test";
            const string dbUrlSourceB = "&source=beta",
            dbUrlSourceT = "&source=test";
            const string spellStart = "https://spells.eqresource.com/spells.php?id=",
            factionStart = "https://factions.eqresource.com/factions.php?id=",
            achieveStart = "https://achievements.eqresource.com/achievements.php?id=",
            itemStart = "https://items.eqresource.com/items.php?id=";

            switch (urlType)
            {
                case "item":
                    outputUrl = itemStart;
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
                case "patch":
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
            }

            return new[] { sourceType, outputUrl, dbSource };
        }

        public static void ReloadValues(string reloadType)
        {
            switch (reloadType)
            {
                case "spell":
                    spellLiveName = ParseFiles.ParseFile("spell", "live");
                    spellTestName = ParseFiles.ParseFile("spell", "test");
                    spellBetaName = ParseFiles.ParseFile("spell", "beta");
                    break;
                case "dbstr":
                    dbStrResultsL = MultiParseFiles.ParseDBStrFiles("live");
                    factionLiveName = dbStrResultsL[0];
                    overseerLiveAgent = dbStrResultsL[1];
                    overseerLiveQuest = dbStrResultsL[2];
                    dbStrResultsT = MultiParseFiles.ParseDBStrFiles("test");
                    factionTestName = dbStrResultsT[0];
                    overseerTestAgent = dbStrResultsT[1];
                    overseerTestQuest = dbStrResultsT[2];
                    dbStrResultsB = MultiParseFiles.ParseDBStrFiles("beta");
                    factionBetaName = dbStrResultsB[0];
                    overseerBetaAgent = dbStrResultsB[1];
                    overseerBetaQuest = dbStrResultsB[2];
                    break;
                case "achieve":
                    achieveLiveName = ParseFiles.ParseFile("achieve", "live");
                    achieveTestName = ParseFiles.ParseFile("achieve", "test");
                    achieveBetaName = ParseFiles.ParseFile("achieve", "beta");
                    break;
                case "item":
                    itemName = ParseFiles.ParseFile("item", "");
                    break;
                case "patch":
                    patchData = ParseFiles.ParsePatchFile();
                    break;
                case "channel":
                    channelsAllowed = LinkBotChannels.AllowedChannels("normal");
                    channelsAllowedAdmin = LinkBotChannels.AllowedChannels("admin");
                    break;
            }
        }
    }
}
