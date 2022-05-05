using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscordBotOffline
{
    class MultiParseFiles
    {
        public static Dictionary<ulong, string>[] ParseDBStrFiles(string multiFileType)
        {
            Dictionary<ulong, string> factionName = new Dictionary<ulong, string>(),
                overseerAgent = new Dictionary<ulong, string>(),
                overseerQuest = new Dictionary<ulong, string>();
            string dbStrFileLoc = string.Empty,
                dbStrFileSource = string.Empty;

            switch (multiFileType)
            {
                case "test":
                    dbStrFileLoc = "dbstr_usT.txt";
                    dbStrFileSource = "Test";
                    break;
                case "beta":
                    dbStrFileLoc = "dbstr_usB.txt";
                    dbStrFileSource = "Beta";
                    break;
                case "live":
                    dbStrFileLoc = "dbstr_usL.txt";
                    dbStrFileSource = "Live";
                    break;
            }

            bool parseFileExists = File.Exists(dbStrFileLoc);

            if (parseFileExists)
            {
                var dbStrLines = File.ReadAllLines(dbStrFileLoc);

                for (int i = 0; i < dbStrLines.Length; i++)
                {
                    var dbStrFields = dbStrLines[i].Split('^');

                    switch (dbStrFields[1])
                    {
                        case "45":
                            factionName.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                            break;
                        case "53":
                            overseerAgent.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                            break;
                        case "56":
                            overseerQuest.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                            break;
                    }
                }
                Globals.CWLMethod($"{dbStrFileSource} Factions: {factionName.Count()}\n{dbStrFileSource} Overseer Agents: {overseerAgent.Count()}\n"
                    + $"{dbStrFileSource} Overseer Quests: {overseerQuest.Count()}", "Magenta");
            }
            else
            {
                factionName.Add(0, "");
                overseerAgent.Add(0, "");
                overseerQuest.Add(0, "");

                Globals.CWLMethod($"{dbStrFileSource} Faction, Overseer Agent, Overseer Quest File Not Found", "Red");
            }

            return new[] { factionName, overseerAgent, overseerQuest };
        }
    }
}
