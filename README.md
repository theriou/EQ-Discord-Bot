# EQ Discord Bot Offline
 EQ Discord Bot that uses Offline files for data instead of pinging Websites\
\
You can see a working version (when EQRLinkBot is online) in the EQ Resource Discord: `https://discord.gg/AwbHXak`\
\
Requires a Discord Bot set up to your discord server, recommended to google this\
\
Requires a config.json file with the following format:\
{\
  "token": "",\
  "prefix": "!"\
}\
Prefix is what you want to see stuff as such as ! = !spell, Token is where your Discord Bot Token goes\
\
Requires an AllowedChannels.txt:\
This file needs 1 channel per line that you would like the bot to Respond to queries with, typing \\#general for example will post the channel ID, or developer discord mode\
\
Requires an AllowedChannelsAdmin.txt:\
This file needs 1 channel per line that you would like the bot to Respond to Reload queries with, this should be in an "Admin" like channel so randoms can't constantly reload files\
\
Requires up to the following EQ files based on which you want to enable/do:\
dbstr_usL.txt - L Live, T Test, B Beta after i.e. dbstr_usL.txt for Live Faction, Overseer Agents, and Overseer Quests\
spells_usL.txt L Live, T Test, B Beta after i.e. spells_usL.txt for Live Spells\
AchievementsClientL.txt - L Live, T Test, B Beta after i.e. AchievementsClientL.txt for Achievements\
itemlist.txt - Items, you can get a public list from https://github.com/theriou/EQ-Item-List \

patch.json - json format Patch example:\
{\
"patch":"All EverQuest Live Servers will be brought offline on Wednesday, September 16, 2020 at 6:00 AM PT* for an update. Downtime is expected to last approximately 7 hours.",\
"date":"2020-09-16T13:00:00.0000000-07:00",\
"link":"`https://eqresource.com\/board\/index.php?topic=208567`"\
}\
You can use a default Link for Update section such as the EQ Forums News and Announcements `https://forums.daybreakgames.com/eq/index.php?forums/news-and-announcements.2/`
\
Raffle:
Requires channel ID's to be in AllowedRaffleChannels.txt file for where the raffle is allowed to take place\
Requires user ID's to be in AllowedRaffleAdmin.txt file these are the members allowed to issue the following commands:\
raffles: Start Raffle - note this will also delete old entries\
rafflee: End Raffle - this will end raffle entries and prepare for the Drawing\
raffler #: Draw Raffle - will accept an int number, can be between 1 and 10, can be rolled unlimited times until a raffles command is done\
\
Note:\
The Text Files will take a couple of seconds to load before outputting data as they build stuff out, after the initial block of load it should be normal until the Program is re-opend.\
\
If Beta Files aren't found it will attempt Test Files, if Test Files are also not found it will attempt Live Files, Live Files at minimum are required to Parse things.
