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
            string[] dataList;
            ulong[] channelList;

            if (channelType == "admin")
            {
                path = @"AllowedChannelsAdmin.txt";
                pathType = "Admin ";
            }
            else
            {
                path = @"AllowedChannels.txt";
            }
            bool channelFile = File.Exists(path);

            if (channelFile)
            {
                dataList = File.ReadAllLines(path);
                channelList = dataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed {pathType}Channels: {channelList.Count()}", "Yellow");
            }
            else
            {
                dataList = new string[] { "0" };
                channelList = dataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed {pathType}Channel File Not Found...", "Red");
            }

            return channelList;
        }

        public static ulong[] AllowedRaffleChannels(string raffleChannelType)
        {
            string raffleType = string.Empty,
                rafflePath = string.Empty;
            string[] raffleDataList;
            ulong[] raffleChannelList;

            if (raffleChannelType == "admin")
            {
                rafflePath = @"AllowedRaffleAdmin.txt";
                raffleType = "Admins";
            }
            else
            {
                rafflePath = @"AllowedRaffleChannels.txt";
                raffleType = "Channels";
            }

            bool raffleFile = File.Exists(rafflePath);

            if (raffleFile)
            {
                raffleDataList = File.ReadAllLines(rafflePath);
                raffleChannelList = raffleDataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed Raffle {raffleType}: {raffleChannelList.Count()}", "Yellow");
            }
            else
            {
                raffleDataList = new string[] { "0" };
                raffleChannelList = raffleDataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed Raffle {raffleType} File Not Found...", "Red");
            }

            return raffleChannelList;
        }
    }
}
