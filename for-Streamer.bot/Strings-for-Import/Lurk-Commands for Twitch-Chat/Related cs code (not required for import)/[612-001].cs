// [612-001] ⫸  clear Group [current_LURK]
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
		string BC_USER = args.ContainsKey("broadcastUser") ? args["broadcastUser"].ToString() : "UnknownUser";
		string TriggerUserBC_Command = "!clearLurker";	// Text, den der BROADCASTER schreibt, damit die Gruppe geleert wird.

		string TriggerUser_1 = "Sery_Bot";	// Benutzername, der den Trigger zum Gruppe-leeren auslöst.
		string TriggerUserTEXT_1 = "Sery_Bot has joined";	// Text, den der Benutzer schreibt, damit die Gruppe geleert wird.

		string TriggerUser_2 = "Michelle_7b7";	// Benutzername, der den Trigger zum Gruppe-leeren auslöst.
		string TriggerUserTEXT_2 = "Lurker Gruppe leeren";	// Text, den der Benutzer schreibt, damit die Gruppe geleert wird.


		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
		string currUserCommand = args.ContainsKey("command") ? args["command"].ToString() : "NoCommand";
		string currUserRawInput = args.ContainsKey("rawInput") ? args["rawInput"].ToString() : "NoRawInput";

		if ((currUser == BC_USER && currUserCommand.Contains(TriggerUserBC_Command)) ||
			(currUser == TriggerUser_1 && currUserRawInput.Contains(TriggerUserTEXT_1)) ||
			(currUser == TriggerUser_2 && currUserRawInput.Contains(TriggerUserTEXT_2)))

			{
				CPH.ClearUsersFromGroup(groupName);
				CPH.SendMessage("/me Gruppe der Lurker geleert...");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
				Thread.Sleep(200);

				var groupUsers = CPH.UsersInGroup(groupName);
				if (groupUsers == null)
				{
					CPH.SendMessage("Fehler: Konnte die Benutzergruppe nicht abrufen.");
					return false;
				}

				int userCount = groupUsers.Count;
				if (userCount == 0)
				{
					CPH.SendMessage("ℹ️ Derzeit ist niemand im Lurk 💤");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
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
