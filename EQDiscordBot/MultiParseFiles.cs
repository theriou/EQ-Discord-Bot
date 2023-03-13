using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQDiscordBot
{
    class MultiParseFiles
    {
        public static Dictionary<ulong, string>[] ParseDBStrFiles(string multiFileType)
        {
            Dictionary<ulong, string> factionName = new Dictionary<ulong, string>(),
                overseerAgent = new Dictionary<ulong, string>(),
                overseerQuest = new Dictionary<ulong, string>();
            string dbStrFileLoc = string.Empty;

            switch (multiFileType)
            {
                case "test":
                    dbStrFileLoc = "data/dbstr_usT.txt";
                    break;
                case "beta":
                    dbStrFileLoc = "data/dbstr_usB.txt";
                    break;
                case "live":
                    dbStrFileLoc = "data/dbstr_usL.txt";
                    break;
            }

            if (File.Exists(dbStrFileLoc))
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
                Globals.CWLMethod($"{multiFileType} Factions: {factionName.Count()}\n{multiFileType} Overseer Agents: {overseerAgent.Count()}\n"
                    + $"{multiFileType} Overseer Quests: {overseerQuest.Count()}", "Magenta");
            }
            else
            {
                factionName.Add(0, "");
                overseerAgent.Add(0, "");
                overseerQuest.Add(0, "");

                Globals.CWLMethod($"{multiFileType} Faction, Overseer Agent, Overseer Quest File Not Found", "Red");
            }

            return new[] { factionName, overseerAgent, overseerQuest };
        }

        public static string[] DataURL()
        {
            string fileType = string.Empty,
                censusURL = string.Empty,
                achieveURL = string.Empty,
                eventURL = string.Empty,
                factionURL = string.Empty,
                itemURL = string.Empty,
                spellURL = string.Empty;
            string dataURLFile = "config/URLData.txt";

            var dataURLLines = File.ReadAllLines(dataURLFile);

            for (int i = 0; i < dataURLLines.Length; i++)
            {
                var dataURLFields = dataURLLines[i].Split('^');

                switch (dataURLFields[0])
                {
                    case "0":
                        censusURL = dataURLFields[1];
                        break;
                    case "1":
                        achieveURL = dataURLFields[1];
                        break;
                    case "2":
                        eventURL = dataURLFields[1];
                        break;
                    case "3":
                        factionURL = dataURLFields[1];
                        break;
                    case "4":
                        itemURL = dataURLFields[1];
                        break;
                    case "5":
                        spellURL = dataURLFields[1];
                        break;
                }
            }
            
            Globals.CWLMethod($"Census URL: {censusURL}\nAchieve URL: {achieveURL}\nEvent URL: {eventURL}\n" +
                $"Faction URL: {factionURL}\nItem URL: {itemURL}\nSpell URL: {spellURL}", "Yellow");

            return new[] { censusURL, achieveURL, eventURL, factionURL, itemURL, spellURL };
        }
    }
}
