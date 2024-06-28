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
		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ Ordnerstruktur und Variablen für Pfase erstellen ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		if (Directory.Exists($@"TXT_for_OBS"))	{
		}	else	{
		Directory.CreateDirectory($@"TXT_for_OBS");
		}
		CPH.SetGlobalVar("PATH_for_TXT_for_OBS", "TXT_for_OBS\\", true);
		CPH.SetGlobalVar("PATH_for_TXT_Filename_Current_Lurker_Count", "Current_Lurker_Count.txt", true);
		CPH.SetGlobalVar("GROUP_NAME_Current_Lurker", "current_LURK", true);
		Thread.Sleep(200);

		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ Count in .txt schreiben ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		string savePath = CPH.GetGlobalVar<string>("PATH_for_TXT_for_OBS");
		string saveName = CPH.GetGlobalVar<string>("PATH_for_TXT_Filename_Current_Lurker_Count");
		string saveFile = (savePath + saveName);
		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		var groupUsersCount = CPH.UsersInGroup(groupName);
		int userCount_int = groupUsersCount.Count;
		string userCount_String = userCount_int.ToString();
		File.WriteAllText(saveFile, userCount_String);
		Thread.Sleep(200);

		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ BROADCASTER in GRUPPEN schreiben ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		Platform platform = Platform.Twitch; 
		string groupName_BROADCASTER = "BROADCASTER";
		TwitchUserInfo broadcasterInfo = CPH.TwitchGetBroadcaster();
		string currBroadcasterUser = broadcasterInfo.UserLogin;
		CPH.AddUserToGroup(currBroadcasterUser, platform, groupName_BROADCASTER);

		CPH.AddUserToGroup(currBroadcasterUser, platform, groupName);
		Thread.Sleep(200);
		CPH.RemoveUserFromGroup(currBroadcasterUser, platform, groupName);
		Thread.Sleep(200);

	return true;
	}
}
