using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiscordBotOffline
{
    class GlobalResults
    {
        public static string TimeLeft(TimeSpan dateParse)
        {
            return $"{dateParse.Days} Days, {dateParse.Hours} Hours, {dateParse.Minutes} Minutes, {dateParse.Seconds} Seconds";
        }

        public static string GlobalResult(string nameSearch, string urlType)
        {
            string[] getGlobalResults = Globals.GetGlobals(urlType),
                getPatchData = Globals.patchData;
            string gotSourceType = getGlobalResults[0],
                gotOutputUrl = getGlobalResults[1],
                gotDbSource = getGlobalResults[2],
                patchDescription = getPatchData[0],
                patchDate = getPatchData[1],
                patchLink = getPatchData[2],
                dataReturn = string.Empty,
                patchOutput = string.Empty;
            Dictionary<ulong, string> searchSource = Globals.GetResults(urlType);

            if (urlType == "patch")
            {
                TimeSpan patchTime = DateTime.Parse(patchDate) - DateTime.Now;

                patchOutput = (patchTime.TotalSeconds < 0) ? "Servers should already be Up!" : $"{TimeLeft(patchTime)} until Servers are Up";

                Globals.CWLMethod($"Patch: {patchDescription}\nDate: {patchDate}\nLink: {patchLink}", "Green");
                dataReturn = $"{patchDescription}\n\n{patchOutput}\n\n[Update Notes and Changes]({patchLink})\n";
            }
            else if (urlType == "event")
            {
                IEnumerable<ParseFiles.EQEvents> eventDataReturn;
                string eventBeginEnd = (nameSearch == "upcoming") ? "Begins" : "Ends";

                if (nameSearch == "upcoming")
                {
                    dataReturn = "Upcoming Events:\n\n";
                    eventDataReturn = Globals.eqEvent.Where(x => x.EventStartDate >= DateTime.Now && x.EventStartDate <= DateTime.Now.AddDays(7)).Take(10);
                }
                else
                {
                    dataReturn = "Active Events:\n\n";
                    eventDataReturn = Globals.eqEvent.Where(x => x.EventStartDate <= DateTime.Now && x.EventEndDate >= DateTime.Now).Take(10);
                }

                if (eventDataReturn.Any() == false)
                {
                    Globals.CWLMethod("No Events Now or Soon", "Red");
                    dataReturn += (nameSearch == "upcoming") ? "No Events begin in the next 7 Days." : "No Events are Currently Active.";
                }
                else
                {
                    foreach (var data in eventDataReturn)
                    {
                        TimeSpan eventTimer = (nameSearch == "upcoming") ? data.EventStartDate - DateTime.Now : data.EventEndDate - DateTime.Now;
                        Globals.CWLMethod($"{data.EventID} + {data.EventName}", "Green");
                        dataReturn += $"[{data.EventName}]({gotOutputUrl}{data.EventID}) - {eventBeginEnd} in {TimeLeft(eventTimer)}";
                    }
                }
            }
            else
            {
                if (urlType == "achieve" || urlType == "achieveb" || urlType == "achievet" ||
                    urlType == "faction" || urlType == "factionb" || urlType == "factiont" ||
                    urlType == "spell" || urlType == "spellb" || urlType == "spellt")
                {
                    dataReturn = $"10 Result Limit for \"{nameSearch}\", Source: {gotDbSource}\n\n";
                }
                else
                {
                    dataReturn = $"10 Result Limit for \"{nameSearch}\"\n\n";
                }
                var searchReturnFilter = searchSource.Where(d => d.Value.ToLower().Contains(nameSearch.ToLower()))
                           .ToDictionary(d => d.Key, d => d.Value).Take(10);

                if (searchReturnFilter.Any() == false)
                {
                    Globals.CWLMethod($"No Results Found for \"{nameSearch}\" in Source: {gotDbSource}", "Red");
                    dataReturn = $"No Results Found for \"{nameSearch}\" in Source: {gotDbSource}";
                }
                else
                {
                    foreach (var result in searchReturnFilter)
                    {
                        Globals.CWLMethod($"ID: {result.Key} - Name: {result.Value}", "Green");
                        dataReturn += $"[{result.Value}]({gotOutputUrl}{result.Key}{gotSourceType})\n";
                    }
                }
            }

            dataReturn = Regex.Replace(dataReturn, @"(?<!(?<!\\)\\(?:\\\\)*)(?=[*_`|~])", @"\");
            return dataReturn;
        }
    }
}
