# EQ Discord Bot Offline
 EQ Discord Bot that uses Offline files for Data instead of pinging Websites for Data\
\
You can see a working version (when EQRLinkBot is online) in the EQ Resource Discord: `https://discord.gg/AwbHXak`\
\
Requires a Discord Bot set up to your discord server, recommended to google this set up process\
\
All files need to be placed in the base programs directory\
\
Requires a config.json file with the following format:\
{\
  "token": "",\
  "prefix": "!"\
}\
Prefix is what you want to see stuff as such as ! = !spell, Token is where your Discord Bot Token goes\
\
Requires an AllowedChannels.txt:\
This file needs 1 channel per line that you would like the bot to Respond to queries with, typing \\#general for example will post the channel ID, or developer discord mode can inspect it\
\
Requires an AllowedChannelsAdmin.txt:\
This file needs 1 channel per line that you would like the bot to Respond to Reload queries with, this should be in an "Admin" like channel so random users can't constantly reload files on you\
The reload command currently accepts: achieve, channel, dbstr (faction + overseer), event, item, patch, spell, all (everything in the list)\
\
Requires up to the following EQ files based on which you want to enable/do:\
dbstr_usL.txt - L Live, T Test, B Beta after i.e. dbstr_usL.txt for Live Faction, Overseer Agents, and Overseer Quests\
spells_usL.txt L Live, T Test, B Beta after i.e. spells_usL.txt for Live Spells\
AchievementsClientL.txt - L Live, T Test, B Beta after i.e. AchievementsClientL.txt for Achievements\
itemlist.txt - Items, you can get a public list from https://github.com/theriou/EQ-Item-List - this will be updated over time\
events.txt - Events, you can get a public list from https://github.com/theriou/EQEvents - this will be updated over time with current Year of Events\
\
patch.json - json format Patch example:\
{\
"patch":"All EverQuest Live Servers will be brought offline on Wednesday, September 16, 2020 at 6:00 AM PT* for an update. Downtime is expected to last approximately 7 hours.",\
"date":"2020-09-16T13:00:00.0000000-07:00",\
"link":"`https://eqresource.com\/board\/index.php?topic=208567`"\
}\
You can use a default Link for Update section such as the EQ Forums News and Announcements `https://forums.daybreakgames.com/eq/index.php?forums/news-and-announcements.2/` in place of the EQResource link\
\
Raffle:\
Requires channel ID's to be in AllowedRaffleChannels.txt file for where the raffle is allowed to take place\
Requires user ID's to be in AllowedRaffleAdmin.txt file you can \\@username to get their ID or use developer Discord to see it\
Members in the AllowedRaffleAdmin file are allowed to issue the following commands:\
raffles: Start Raffle - note this will also delete old entries if a raffle was running, so don't run it twice\
rafflee: End Raffle - this will end raffle entry acceptance and prepare for the Drawing\
raffler #: Draw Raffle - will accept an int number, will default between 1 and 10, if you need more it can be re-rolled unlimited times until a raffles command is re-entered (to reset raffle) or bot is reloaded\
\
Most results for commands are limited to 10 results to fit safely within Discords per message length limits and to be reasonable length list results.
\
If Beta Files aren't found it will attempt Test Files, if Test Files are also not found it will attempt Live Files.
