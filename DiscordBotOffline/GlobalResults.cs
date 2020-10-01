using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiscordBotOffline
{
    public class PatchJson
    {
        public string Patch { get; set; }
        public string Date { get; set; }
        public string Link { get; set; }
    }
    class GlobalResults
    {
        public static string GlobalResult(string nameSearch, string urlType)
        {
            string[] getGlobalResults = Globals.GetGlobals(urlType);
            string gotSourceType = getGlobalResults[0],
                gotOutputUrl = getGlobalResults[1],
                gotDbSource = getGlobalResults[2];
            int listCap;
            Dictionary<ulong,string> searchSource = Globals.GetResults(urlType);

            if (urlType == "patch")
            {
                PatchJson patchFile = JsonConvert.DeserializeObject<PatchJson>(File.ReadAllText(@"patch.json"));

                string patchOutput = string.Empty,
                        patchDescription = patchFile.Patch,
                        patchDate = patchFile.Date,
                        patchLink = patchFile.Link;

                TimeSpan patchTime = DateTime.Parse(patchDate) - DateTime.Now;
                if (patchTime.TotalSeconds < 0)
                {
                    patchOutput = "Servers should already be up!";
                }
                else
                {
                    string output = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds",
                        patchTime.Days, patchTime.Hours, patchTime.Minutes, patchTime.Seconds);
                    patchOutput = output + " Until Servers Up";
                }

                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Patch: "); Console.ResetColor(); Console.Write(patchDescription + "\n");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Date: "); Console.ResetColor(); Console.Write(patchDate + "\n");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Link: "); Console.ResetColor(); Console.Write(patchLink + "\n");

                string patchReturn = "" + patchDescription + "\n\n" + patchOutput + "\n\n[Update Notes and Changes](" + patchLink + ")\n";
                patchReturn = Regex.Replace(patchReturn, @"(?<!(?<!\\)\\(?:\\\\)*)(?=[*_`|~])", @"\");
                return patchReturn;
            }
            else
            {
                string searchReturn = string.Empty;

                if (urlType == "spell" || urlType == "spellt" || urlType == "spellb" ||
                    urlType == "achieve" || urlType == "achievet" || urlType == "achieveb" ||
                    urlType == "faction" || urlType == "factiont" || urlType == "factionb")
                {
                    searchReturn = $"10 Result Limit for \"{nameSearch}\", Source: {gotDbSource}\n\n";
                }
                else
                {
                    searchReturn = $"10 Result Limit for \"{nameSearch}\"\n\n";
                }
                listCap = 0;
                var searchReturnFilter = searchSource.Where(d => d.Value.ToLower().Contains(nameSearch.ToLower()))
                           .ToDictionary(d => d.Key, d => d.Value);

                foreach (var result in searchReturnFilter)
                {
                    if (listCap <= 9)
                    {
                        searchReturn += "[" + result.Value + "](" + gotOutputUrl + result.Key + gotSourceType + ")\n";
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ID: {0} - Name: {1}", result.Key, result.Value); Console.ResetColor();
                        listCap++;
                    }
                    else
                    {
                        break;
                    }
                }

                searchReturn = Regex.Replace(searchReturn, @"(?<!(?<!\\)\\(?:\\\\)*)(?=[*_`|~])", @"\");
                return searchReturn;
            }
        }
    }
}
