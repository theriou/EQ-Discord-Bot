using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiscordBotOffline
{

    class GlobalResults
    {
        public static string GlobalResult(string nameSearch, string urlType)
        {
            string[] getGlobalResults = Globals.GetGlobals(urlType);
            string gotSourceType = getGlobalResults[0],
                gotOutputUrl = getGlobalResults[1],
                gotDbSource = getGlobalResults[2];
            string[] getPatchData = Globals.patchData;
            string patchDescription = getPatchData[0],
                patchDate = getPatchData[1],
                patchLink = getPatchData[2];
            int listCap;
            Dictionary<ulong,string> searchSource = Globals.GetResults(urlType);

            if (urlType == "patch")
            {
                string patchOutput = string.Empty;

                TimeSpan patchTime = DateTime.Parse(patchDate) - DateTime.Now;
                if (patchTime.TotalSeconds < 0)
                {
                    patchOutput = "Servers should already be up!";
                }
                else
                {
                    string output = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds",
                        patchTime.Days, patchTime.Hours, patchTime.Minutes, patchTime.Seconds);
                    patchOutput = $"{output} Until Servers Up";
                }

                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Patch: "); Console.ResetColor(); Console.Write(patchDescription + "\n");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Date: "); Console.ResetColor(); Console.Write(patchDate + "\n");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Link: "); Console.ResetColor(); Console.Write(patchLink + "\n");

                string patchReturn = $"{patchDescription}\n\n{patchOutput}\n\n[Update Notes and Changes]({patchLink})\n";
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
                        searchReturn += $"[{result.Value}]({gotOutputUrl}{result.Key}{gotSourceType})\n";
                        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"ID: {result.Key} - Name: {result.Value}"); Console.ResetColor();
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
