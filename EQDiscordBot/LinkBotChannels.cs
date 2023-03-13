using System.IO;
using System.Linq;

namespace EQDiscordBot
{
    class LinkBotChannels
    {
        public static ulong[] AllowedChannelMessages(string channelType, string fileType)
        {
            string path = string.Empty;
            string[] dataList;
            ulong[] channelList;

            if (channelType == "admin")
            {
                switch (fileType)
                {
                    case "channels":
                        path = "config/AllowedChannelsAdmin.txt";
                        break;
                    case "raffle":
                        path = "config/AllowedRaffleAdmin.txt";
                        break;
                }
            }
            else
            {
                switch (fileType)
                {
                    case "channels":
                        path = "config/AllowedChannels.txt";
                        break;
                    case "raffle":
                        path = "config/AllowedRaffleChannels.txt";
                        break;
                    case "roles":
                        path = "config/AllowedRoleMessages.txt";
                        break;
                }
            }

            if (File.Exists(path))
            {
                dataList = File.ReadAllLines(path);
                channelList = dataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed {channelType} {fileType}: {channelList.Count()}", "Yellow");
            }
            else
            {
                dataList = new string[] { "0" };
                channelList = dataList.Select(x => ulong.Parse(x)).ToArray();
                Globals.CWLMethod($"Allowed {channelType} {fileType} File Not Found...", "Red");
            }

            return channelList;
        }

        public static ulong MessageChannelID()
        {
            ulong messageDataList;

            string messagePath = "config/ServerChannelId.txt";

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
