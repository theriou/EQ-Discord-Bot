using System;
using System.Collections.Generic;
using System.Net.Http;

namespace EQDiscordBot
{
    class Globals
    {
        public static string loadBotFiles = string.Empty;
        public static readonly HttpClient StatusClient = new HttpClient();

        public static string[] urlResults = MultiParseFiles.DataURL();
        public static string censusURL = urlResults[0];
        public static string achieveURL = urlResults[1];
        public static string eventURL = urlResults[2];
        public static string factionURL = urlResults[3];
        public static string itemURL = urlResults[4];
        public static string spellURL = urlResults[5];

        public static ulong[] channelsAllowedAdmin = LinkBotChannels.AllowedChannelMessages("admin", "channels");
        public static ulong[] channelsAllowed = LinkBotChannels.AllowedChannelMessages("", "channels");
        public static ulong[] raffleChannelsAdmins = LinkBotChannels.AllowedChannelMessages("admin", "raffle");
        public static ulong[] raffleChannelsAllowed = LinkBotChannels.AllowedChannelMessages("", "raffle");
        public static ulong[] roleMessagesAllowed = LinkBotChannels.AllowedChannelMessages("", "roles");
        public static ulong messageID = LinkBotChannels.MessageChannelID();

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

        public static Dictionary<string, ulong> rolesName = ParseFiles.ParseRolesFile();
        public static List<ParseFiles.ServersAndRoles> serverStatus = ParseFiles.ParseServerRoleFile();

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
            string dbSourceB = "Beta",
                dbSourceL = "Live",
                dbSourceT = "Test",
                dbUrlSourceB = "&source=beta",
                dbUrlSourceT = "&source=test";

            switch (urlType)
            {
                case "achieve":
                    outputUrl = achieveURL;
                    dbSource = dbSourceL;
                    break;
                case "achievet":
                    sourceType = dbUrlSourceT;
                    outputUrl = achieveURL;
                    dbSource = dbSourceT;
                    break;
                case "achieveb":
                    sourceType = dbUrlSourceB;
                    outputUrl = achieveURL;
                    dbSource = dbSourceB;
                    break;
                case "event":
                    outputUrl = eventURL;
                    break;
                case "faction":
                    outputUrl = factionURL;
                    dbSource = dbSourceL;
                    break;
                case "factiont":
                    sourceType = dbUrlSourceT;
                    outputUrl = factionURL;
                    dbSource = dbSourceT;
                    break;
                case "factionb":
                    sourceType = dbUrlSourceB;
                    outputUrl = factionURL;
                    dbSource = dbSourceB;
                    break;
                case "item":
                    outputUrl = itemURL;
                    break;
                case "patch":
                    break;
                case "spell":
                    outputUrl = spellURL;
                    dbSource = dbSourceL;
                    break;
                case "spellt":
                    sourceType = dbUrlSourceT;
                    outputUrl = spellURL;
                    dbSource = dbSourceT;
                    break;
                case "spellb":
                    sourceType = dbUrlSourceB;
                    outputUrl = spellURL;
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
                    channelsAllowedAdmin = LinkBotChannels.AllowedChannelMessages("admin", "channels");
                    channelsAllowed = LinkBotChannels.AllowedChannelMessages("", "channels");
                    raffleChannelsAdmins = LinkBotChannels.AllowedChannelMessages("admin", "raffle");
                    raffleChannelsAllowed = LinkBotChannels.AllowedChannelMessages("", "raffle");
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
                case "event":
                    eqEvent = ParseFiles.ParseEventFile();
                    break;
                case "item":
                    itemName = ParseFiles.ParseFile("item", "");
                    break;
                case "patch":
                    patchData = ParseFiles.ParsePatchFile();
                    break;
                case "role":
                    roleMessagesAllowed = LinkBotChannels.AllowedChannelMessages("", "roles");
                    messageID = LinkBotChannels.MessageChannelID();
                    rolesName = ParseFiles.ParseRolesFile();
                    serverStatus = ParseFiles.ParseServerRoleFile();
                    break;
                case "spell":
                    spellBetaName = ParseFiles.ParseFile("spell", "beta");
                    spellLiveName = ParseFiles.ParseFile("spell", "live");
                    spellTestName = ParseFiles.ParseFile("spell", "test");
                    break;
                case "url":
                    urlResults = MultiParseFiles.DataURL();
                    censusURL = urlResults[0];
                    achieveURL = urlResults[1];
                    eventURL = urlResults[2];
                    factionURL = urlResults[3];
                    itemURL = urlResults[4];
                    spellURL = urlResults[5];
                    break;
            }
        }

        public static void CWLMethod(string cwlText, string cwlColor)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cwlColor); Console.WriteLine($"[{DateTime.Now}] - {cwlText}"); Console.ResetColor();
        }
    }
}
