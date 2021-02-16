using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscordBotOffline
{
    class ParseFiles
    {
        public static Dictionary<ulong, string> ParseFile(string fileType, string fileSource)
        {
            Dictionary<ulong, string> parseName = new Dictionary<ulong, string>();
            string parseFileLoc = string.Empty, 
                parseFileSource = string.Empty;

            if (fileType == "achieve")
            {
                switch (fileSource)
                {
                    case "test":
                        parseFileLoc = "AchievementsClientT.txt";
                        parseFileSource = "Test Achievements";
                        break;
                    case "beta":
                        parseFileLoc = "AchievementsClientB.txt";
                        parseFileSource = "Beta Achievements";
                        break;
                    default:
                        parseFileLoc = "AchievementsClientL.txt";
                        parseFileSource = "Live Achievements";
                        break;
                }
            }
            if (fileType == "item")
            {
                parseFileLoc = "itemlist.txt";
                parseFileSource = "Items";
            }
            if (fileType == "spell")
            {
                switch (fileSource)
                {
                    case "test":
                        parseFileLoc = "spells_usT.txt";
                        parseFileSource = "Test Spells";
                        break;
                    case "beta":
                        parseFileLoc = "spells_usB.txt";
                        parseFileSource = "Beta Spells";
                        break;
                    default:
                        parseFileLoc = "spells_usL.txt";
                        parseFileSource = "Live Spells";
                        break;
                }
            }

            bool parseFileExists = File.Exists(parseFileLoc);

            if (parseFileExists)
            {
                var parseLines = File.ReadAllLines(parseFileLoc);

                for (int i = 0; i < parseLines.Length; i++)
                {
                    var parseFields = parseLines[i].Split('^');
                    parseName.Add(ulong.Parse(parseFields[0]), parseFields[1]);
                }

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{parseFileSource} Count: {parseName.Count()}"); Console.ResetColor();

            }
            else
            {
                parseName.Add(0, "Null");
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{parseFileSource} File Not Found..."); Console.ResetColor();

            }

            return parseName;
        }

        public class PatchJson
        {
            public string Patch { get; set; }
            public string Date { get; set; }
            public string Link { get; set; }
        }

        public static string[] ParsePatchFile()
        {
            string parsePatchFileLoc = string.Empty;

            parsePatchFileLoc = "patch.json";

            bool parsePatchFileExists = File.Exists(parsePatchFileLoc);

            if (parsePatchFileExists)
            {
                PatchJson patchFile = JsonConvert.DeserializeObject<PatchJson>(File.ReadAllText(@"patch.json"));
                string patchOutput = string.Empty,
                            patchDescription = patchFile.Patch,
                            patchDate = patchFile.Date,
                            patchLink = patchFile.Link;

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"Patch File Loaded"); Console.ResetColor();

                return new[] { patchDescription, patchDate, patchLink };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"Patch File Not Found..."); Console.ResetColor();
                return null;
            }
        }

        public class EQREvents
        {
            public int EventID { get; set; }
            public string EventName { get; set; }
            public DateTime EventStartDate { get; set; }
            public DateTime EventEndDate { get; set; }
        }

        public static List<EQREvents> ParseEventFile()
        {
            string parseEventFileLoc = string.Empty;
            List<EQREvents> eqEventData = new List<EQREvents>();

            parseEventFileLoc = "events.txt";

            bool parseEventFileExists = File.Exists(parseEventFileLoc);

            if (parseEventFileExists)
            {
                var parseEventLines = File.ReadAllLines(parseEventFileLoc);

                for (int i = 0; i < parseEventLines.Length; i++)
                {
                    var parseEventFields = parseEventLines[i].Split('^');

                    eqEventData.Add(new EQREvents() { EventID = Int32.Parse(parseEventFields[0]), EventName = parseEventFields[1],
                        EventStartDate = DateTime.Parse(parseEventFields[2]), EventEndDate = DateTime.Parse(parseEventFields[3]) });
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"Event Count: {eqEventData.Count()}"); Console.ResetColor();

            }
            else
            {
                eqEventData = null;
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Event File Not Found..."); Console.ResetColor();

            }

            return eqEventData;
        }

    }
}
