using System;
using System.IO;
using System.Linq;

namespace DiscordBotOffline
{
    class LinkBotChannels
    {
        public static ulong[] AllowedChannels()
        {
            string path = @"AllowedChannels.txt";
            bool channelFile = File.Exists(path);

            if (channelFile)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Allowed Channel File Found"); Console.ResetColor();
                string[] dataList = File.ReadAllLines(path);
                ulong[] channelList = dataList.Select(x => ulong.Parse(x)).ToArray();

                return channelList;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Allowed Channel File Not Found..."); Console.ResetColor();
                string[] channelFileNotFound = new string[] { "0" };
                ulong[] channelNotFound = channelFileNotFound.Select(i => ulong.Parse(i)).ToArray();
                    
                return channelNotFound;
            }
        }
    }
}
