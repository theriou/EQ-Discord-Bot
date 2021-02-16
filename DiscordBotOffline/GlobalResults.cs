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
            string dataReturn = string.Empty;
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

                dataReturn = $"{patchDescription}\n\n{patchOutput}\n\n[Update Notes and Changes]({patchLink})\n";
            }
            else if (urlType == "event")
            {
                if (nameSearch == "upcoming")
                {
                    dataReturn = "Upcoming Events:\n\n";
                    var eventDataReturn = Globals.eqrEvent.Where(x => x.EventStartDate >= DateTime.Now && x.EventStartDate <= DateTime.Now.AddDays(7));

                    if (eventDataReturn.Any() == false)
                    {
                        dataReturn += "No Events begin in the next 7 Days.";
                    }
                    else
                    {
                        foreach (var data in eventDataReturn)
                        {
                            TimeSpan eventTimer = data.EventStartDate - DateTime.Now;
                            string eventTime = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds",
                                eventTimer.Days, eventTimer.Hours, eventTimer.Minutes, eventTimer.Seconds);
                            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"{data.EventID} + {data.EventName}"); Console.ResetColor();
                            dataReturn += $"[{data.EventName}]({gotOutputUrl}{data.EventID}) - Begins in {eventTime}";
                        }
                    }
                }
                else
                {
                    dataReturn = "Active Events:\n\n";
                    var eventDataReturn = Globals.eqrEvent.Where(x => x.EventStartDate <= DateTime.Now && x.EventEndDate >= DateTime.Now);

                    if (eventDataReturn.Any() == false)
                    {
                        dataReturn += "No Events are Currently Active.";
                    }
                    else
                    {
                        foreach (var data in eventDataReturn)
                        {
                            TimeSpan eventTimer = data.EventEndDate - DateTime.Now;
                            string eventTime = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds",
                                eventTimer.Days, eventTimer.Hours, eventTimer.Minutes, eventTimer.Seconds);
                            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"{data.EventID} + {data.EventName}"); Console.ResetColor();
                            dataReturn += $"[{data.EventName}]({gotOutputUrl}{data.EventID}) - Ends in {eventTime}";
                        }
                    }
                }
            }
            else
            {
                if (urlType == "achieve" || urlType == "achievet" || urlType == "achieveb" ||
                    urlType == "faction" || urlType == "factiont" || urlType == "factionb" ||
                    urlType == "spell" || urlType == "spellt" || urlType == "spellb")
                {
                    dataReturn = $"10 Result Limit for \"{nameSearch}\", Source: {gotDbSource}\n\n";
                }
                else
                {
                    dataReturn = $"10 Result Limit for \"{nameSearch}\"\n\n";
                }
                listCap = 0;
                var searchReturnFilter = searchSource.Where(d => d.Value.ToLower().Contains(nameSearch.ToLower()))
                           .ToDictionary(d => d.Key, d => d.Value);

                if (searchReturnFilter.Any())
                {
                    foreach (var result in searchReturnFilter)
                    {
                        if (listCap <= 9)
                        {
                            dataReturn += $"[{result.Value}]({gotOutputUrl}{result.Key}{gotSourceType})\n";
                            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"ID: {result.Key} - Name: {result.Value}"); Console.ResetColor();
                            listCap++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    dataReturn += "No Results Found";
                }
            }

            dataReturn = Regex.Replace(dataReturn, @"(?<!(?<!\\)\\(?:\\\\)*)(?=[*_`|~])", @"\");
            return dataReturn;
        }
    }
}
