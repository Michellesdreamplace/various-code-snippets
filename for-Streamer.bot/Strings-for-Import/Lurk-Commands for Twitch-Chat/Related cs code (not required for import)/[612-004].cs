// [612-004] ⫸  Lurker, die etwas schreiben --> UNLURK
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
		Platform platform = Platform.Twitch;
		string LURK_Message_UnlurkPerText = CPH.GetGlobalVar<string>("LURK_Message_UnlurkPerText");
		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
		string currUserRawInput = args.ContainsKey("rawInput") ? args["rawInput"].ToString() : "NoRawInput";

		// Weitere Logik für "Lurker" anhand einer Chat-Nachricht
		if (CPH.UserInGroup(currUser, platform, groupName))
		{
			if (currUserRawInput.Contains("!lurk") || currUserRawInput.Contains("!unlurk") || currUserRawInput.Contains("!lurker"))	{
				}	else	{
					// Lurker, die irgendwas schreiben
					CPH.RemoveUserFromGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage("wb " + currUser + LURK_Message_UnlurkPerText);	// ◄◄◄◄◄ HIER kann der Text-Anfang angepasst werden
				}
		}

		string savePath = CPH.GetGlobalVar<string>("PATH_for_TXT_for_OBS");
		string saveName = CPH.GetGlobalVar<string>("PATH_for_TXT_Filename_Current_Lurker_Count");
		string saveFile = (savePath + saveName);
		var groupUsersCount = CPH.UsersInGroup(groupName);
		int userCount_int = groupUsersCount.Count;
		string userCount_String = userCount_int.ToString();
		File.WriteAllText(saveFile, userCount_String);

		return true;
	}
}
