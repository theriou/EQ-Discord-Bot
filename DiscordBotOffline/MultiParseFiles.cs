using System;
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
                default:
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

                    if (dbStrFields[1] == "45")
                    {
                        factionName.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                    }
                    if (dbStrFields[1] == "53")
                    {
                        overseerAgent.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                    }
                    if (dbStrFields[1] == "56")
                    {
                        overseerQuest.Add(ulong.Parse(dbStrFields[0]), dbStrFields[2]);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{dbStrFileSource} Faction Count: {factionName.Count()}"); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{dbStrFileSource} Overseer Agent Count: {overseerAgent.Count()}"); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{dbStrFileSource} Overseer Quest Count: {overseerQuest.Count()}"); Console.ResetColor();
            }
            else
            {
                factionName.Add(0, "Null");
                overseerAgent.Add(0, "Null");
                overseerQuest.Add(0, "Null");

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{dbStrFileSource} Faction, Overseer Agent, Overseer Quest File Not Found"); Console.ResetColor();
            }

            return new[] { factionName, overseerAgent, overseerQuest };
        }
    }
}
