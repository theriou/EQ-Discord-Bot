using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQDiscordBot
{
    class ParseFiles
    {
        public static Dictionary<ulong, string> ParseFile(string fileType, string fileSource)
        {
            Dictionary<ulong, string> parseName = new Dictionary<ulong, string>();
            string parseFileLoc = string.Empty;

            if (fileType == "achieve")
            {
                switch (fileSource)
                {
                    case "test":
                        parseFileLoc = "data/AchievementsClientT.txt";
                        break;
                    case "beta":
                        parseFileLoc = "data/AchievementsClientB.txt";
                        break;
                    case "live":
                        parseFileLoc = "data/AchievementsClientL.txt";
                        break;
                }
            }
            if (fileType == "item")
            {
                parseFileLoc = "data/itemlist.txt";
            }
            if (fileType == "spell")
            {
                switch (fileSource)
                {
                    case "test":
                        parseFileLoc = "data/spells_usT.txt";
                        break;
                    case "beta":
                        parseFileLoc = "data/spells_usB.txt";
                        break;
                    case "live":
                        parseFileLoc = "data/spells_usL.txt";
                        break;
                }
            }

            if (File.Exists(parseFileLoc))
            {
                var parseLines = File.ReadAllLines(parseFileLoc);

                for (int i = 0; i < parseLines.Length; i++)
                {
                    var parseFields = parseLines[i].Split('^');
                    parseName.Add(ulong.Parse(parseFields[0]), parseFields[1]);
                }

                Globals.CWLMethod($"{fileSource} {fileType}: {parseName.Count()}", "Magenta");

            }
            else
            {
                parseName.Add(0, "");
                Globals.CWLMethod($"{fileSource} {fileType} File Not Found...", "Red");

            }

            return parseName;
        }

        public static string[] ParsePatchFile()
        {
            string[] eqPatchData;
            string parsePatchFileLoc = "data/patch.json";

            if (File.Exists(parsePatchFileLoc))
            {
                JObject patchFile = JObject.Parse(File.ReadAllText(parsePatchFileLoc));

                Globals.CWLMethod("Patch File Loaded", "Magenta");

                eqPatchData = new[] { patchFile["patch"].ToString(), patchFile["startdate"].ToString(), patchFile["enddate"].ToString(), patchFile["link"].ToString() };
            }
            else
            {
                Globals.CWLMethod("Patch File Not Found...", "Red");
                eqPatchData = null;
            }

            return eqPatchData;
        }

        public static Dictionary<string, string> ParseURLFile()
        {
            Dictionary<string, string> urlList = new Dictionary<string, string>();
            string parseParseFileLoc = "config/URLData.json";
            string[] urlTypes = { "achieve", "census", "event", "faction", "item", "spell" };

            if (File.Exists(parseParseFileLoc))
            {
                JObject urlFile = JObject.Parse(File.ReadAllText(parseParseFileLoc));

                Globals.CWLMethod("URL Data File Loaded", "Magenta");

                foreach (string url in urlTypes)
                {
                   if (urlFile.ContainsKey(url))
                    {
                        urlList.Add(url, urlFile[url].ToString());
                        urlTypes = urlTypes.Where(u => u != url).ToArray();
                    }
                }
            }
            else
            {
                Globals.CWLMethod("URL Data File Not Found...", "Red");
            }

            foreach(string url in urlTypes)
            {
                Globals.CWLMethod($"{url} URL Data Missing", "Red");
                urlList.Add(url, "");
            }

            return urlList;
        }

        public class EQEvents
        {
            public int EventID { get; set; }
            public string EventName { get; set; }
            public DateTime EventStartDate { get; set; }
            public DateTime EventEndDate { get; set; }
        }

        public static List<EQEvents> ParseEventFile()
        {
            List<EQEvents> eqEventData = new List<EQEvents>();
            string parseEventFileLoc = "data/events.txt";

            if (File.Exists(parseEventFileLoc))
            {
                var parseEventLines = File.ReadAllLines(parseEventFileLoc);

                for (int i = 0; i < parseEventLines.Length; i++)
                {
                    var parseEventFields = parseEventLines[i].Split('^');

                    eqEventData.Add(new EQEvents()
                    {
                        EventID = Int32.Parse(parseEventFields[0]),
                        EventName = parseEventFields[1],
                        EventStartDate = DateTime.Parse(parseEventFields[2]),
                        EventEndDate = DateTime.Parse(parseEventFields[3])
                    });
                }

                Globals.CWLMethod($"Events: {eqEventData.Count()}", "Magenta");
            }
            else
            {
                eqEventData = null;
                Globals.CWLMethod("Event File Not Found...", "Red");
            }

            return eqEventData;
        }

        public static Dictionary<string, ulong> ParseRolesFile()
        {
            Dictionary<string, ulong> rolesName = new Dictionary<string, ulong>();
            string roleFileLoc = "config/ServerRoles.txt";

            if (File.Exists(roleFileLoc))
            {
                var roleLines = File.ReadAllLines(roleFileLoc);

                for (int i = 0; i < roleLines.Length; i++)
                {
                    var roleFields = roleLines[i].Split('^');
                    rolesName.Add(roleFields[0], ulong.Parse(roleFields[1]));
                }

                Globals.CWLMethod($"{rolesName.Count()} Member Roles Found", "Yellow");
            }
            else
            {
                rolesName.Add("", 0);
                Globals.CWLMethod($"Roles File Not Found...", "Red");
            }

            return rolesName;
        }

        public class ServersAndRoles
        {
            public string ServerRegion { get; set; }
            public string ServerName { get; set; }
            public ulong RolesID { get; set; }
            public string ServerStatus { get; set; }
        }


        public static List<ServersAndRoles> ParseServerRoleFile()
        {
            List<ServersAndRoles> serverRoles = new List<ServersAndRoles>();
            var eqResults = "0";
            string serverRoleFileLoc = "config/ServerJSON.txt";

            if (File.Exists(serverRoleFileLoc))
            {
                var roleLines = File.ReadAllLines(serverRoleFileLoc);

                for (int i = 0; i < roleLines.Length; i++)
                {
                    var serverRoleFields = roleLines[i].Split('^');
                    serverRoles.Add(new ServersAndRoles() 
                    { 
                        ServerRegion = serverRoleFields[0], 
                        ServerName = serverRoleFields[1], 
                        RolesID = ulong.Parse(serverRoleFields[2]) 
                    });
                }

                Globals.CWLMethod($"{serverRoles.Count()} Servers Found, Parsing Initial Status", "Yellow");

                try
                {
                    if (string.IsNullOrEmpty(Globals.censusURL))
                    {
                        eqResults = null;
                    }
                    else
                    {
                        eqResults = Globals.StatusClient.GetStringAsync(Globals.censusURL).Result;
                    }

                    if (string.IsNullOrEmpty(eqResults) || (!eqResults.StartsWith("{") && !eqResults.EndsWith("}")))
                    {
                        Globals.CWLMethod($"Null or Bad Initial Census Data Received", "Red");
                    }
                    else
                    {
                        JObject eqStatusResult = JObject.Parse(eqResults);

                        foreach (var statusResults in serverRoles)
                        {
                            statusResults.ServerStatus = eqStatusResult["eq"][statusResults.ServerRegion][statusResults.ServerName]["status"].ToString();
                        }

                        Globals.CWLMethod($"Server Status Values Initiated", "Yellow");
                    }
                }
                catch
                {
                    Globals.CWLMethod($"Server Status Values Failed to Initiate", "Red");
                }

            }
            else
            {
                serverRoles.Add(new ServersAndRoles() { ServerRegion = null, ServerName = null, RolesID = 0 });
                Globals.CWLMethod($"Servers And Roles File Not Found...", "Red");
            }

            return serverRoles;
        }

    }
}
