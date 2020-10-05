﻿using System;
using System.IO;
using System.Linq;

namespace DiscordBotOffline
{
    class LinkBotChannels
    {
        public static ulong[] AllowedChannels(string channelType)
        {
            string path = string.Empty,
                pathType = string.Empty;
            if (channelType == "normal")
            {
                path = @"AllowedChannels.txt";
            }
            else
            {
                path = @"AllowedChannelsAdmin.txt";
                pathType = "Admin ";
            }
            bool channelFile = File.Exists(path);

            if (channelFile)
            {
                string[] dataList = File.ReadAllLines(path);
                ulong[] channelList = dataList.Select(x => ulong.Parse(x)).ToArray();
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"Allowed {pathType}Channel: {channelList.Count()}"); Console.ResetColor();

                return channelList;
            }
            else
            {
                string[] channelFileNotFound = new string[] { "0" };
                ulong[] channelNotFound = channelFileNotFound.Select(i => ulong.Parse(i)).ToArray();
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Allowed {pathType}Channel File Not Found..."); Console.ResetColor();

                return channelNotFound;
            }
        }
    }
}
