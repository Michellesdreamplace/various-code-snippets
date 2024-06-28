// [612-000] ⫸  LURK - First Start
// v1.171 by Michelle_7b7
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface.Model;

public class CPHInline
{
    public bool Execute()
    {
		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ BROADCASTER in GRUPPEN schreiben ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		Platform platform = Platform.Twitch; 
		string groupName_BROADCASTER = "BROADCASTER";
		TwitchUserInfo broadcasterInfo = CPH.TwitchGetBroadcaster();
		string currBroadcasterUser = broadcasterInfo.UserLogin;
		CPH.AddUserToGroup(currBroadcasterUser, platform, groupName_BROADCASTER);
		//▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲ BROADCASTER in GRUPPEN schreiben ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

	return true;
    }
}
