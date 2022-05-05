using System.IO;
using System.Linq;

namespace EQDiscordBot
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
                path = @"config/AllowedChannelsAdmin.txt";
                pathType = "Admin ";
            }
            else
            {
                path = @"config/AllowedChannels.txt";
            }

            if (File.Exists(path))
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
                rafflePath = @"config/AllowedRaffleAdmin.txt";
                raffleType = "Admins";
            }
            else
            {
                rafflePath = @"config/AllowedRaffleChannels.txt";
                raffleType = "Channels";
            }

            if (File.Exists(rafflePath))
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

        public static ulong[] AllowedRoleMessages(string roles)
        {
            string path = string.Empty;
            string[] roleDataList;
            ulong[] messageList;

            path = @"config/AllowedRoleMessages.txt";

            if (File.Exists(path))
            {
                roleDataList = File.ReadAllLines(path);
                messageList = roleDataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed Server Role Messages: {messageList.Count()}", "Yellow");
            }
            else
            {
                roleDataList = new string[] { "0" };
                messageList = roleDataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed Server Role Messages File Not Found...", "Red");
            }

            return messageList;
        }

        public static ulong MessageChannelID(string roleMessageType)
        {
            string messagePath = string.Empty;
            ulong messageDataList;

            messagePath = @"config/ServerChannelId.txt";

            if (File.Exists(messagePath))
            {
                messageDataList = ulong.Parse(File.ReadLines(messagePath).First());
                Globals.CWLMethod($"Channel ID: {messageDataList}", "Yellow");
            }
            else
            {
                messageDataList = 0;
                Globals.CWLMethod($"Channel ID Message File Not Found...", "Red");
            }

            return messageDataList;
        }
    }
}
