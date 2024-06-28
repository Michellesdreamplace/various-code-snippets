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
		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
        string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
        string currUserRawInput = args.ContainsKey("rawInput") ? args["rawInput"].ToString() : "NoRawInput";
     
			// Weitere Logik für "Lurker" anhand einer Chat-Nachricht
			if (CPH.UserInGroup(currUser, platform, groupName))
			{
				if (currUserRawInput.Contains("!lurk") || currUserRawInput.Contains("!unlurk") || currUserRawInput.Contains("!lurker"))
				{
					// Lurker, die einen der o.g. Befehle schreiben
				}
				else
				{
					// Lurker, die irgendwas schreiben
					CPH.RemoveUserFromGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage("wb " + currUser + " <3 Schön, dass du wieder aus dem Lurk zurück bist <3 <3 <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
					//return true;
				}
			}

		// Count in .txt schreiben
		string savePath = CPH.GetGlobalVar<string>("TXT_for_OBS_Pfad");
		string saveName = CPH.GetGlobalVar<string>("TXT_Filename_Current_Lurker_Count");
		string saveFile = (savePath + saveName);
		var groupUsersCount = CPH.UsersInGroup(groupName);
		int userCount_int = groupUsersCount.Count;
		string userCount_String = userCount_int.ToString();
		File.WriteAllText(saveFile, userCount_String);

		return true;
    }
}
