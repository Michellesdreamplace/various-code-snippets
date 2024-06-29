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
		string LURK_Message_meVoranstellen = CPH.GetGlobalVar<string>("LURK_Message_meVoranstellen");
		string LURK_TriggerUser_1 = CPH.GetGlobalVar<string>("LURK_TriggerUser_1");
		string LURK_TriggerUserTEXT_1 = CPH.GetGlobalVar<string>("LURK_TriggerUserTEXT_1");
		string LURK_TriggerUser_2 = CPH.GetGlobalVar<string>("LURK_TriggerUser_2");
		string LURK_TriggerUserTEXT_2 = CPH.GetGlobalVar<string>("LURK_TriggerUserTEXT_2");
		
		string BC_USER = args.ContainsKey("broadcastUser") ? args["broadcastUser"].ToString() : "UnknownUser";
		string TriggerUserBC_Command = "!clearLurker";	// Text, den der BROADCASTER schreibt, damit die Gruppe geleert wird ►►►[als COMMAND aktiviert]
		string TriggerUser_1 = LURK_TriggerUser_1;
		string TriggerUserTEXT_1 = LURK_TriggerUserTEXT_1;
		string TriggerUser_2 = LURK_TriggerUser_2;
		string TriggerUserTEXT_2 = LURK_TriggerUserTEXT_2;

		string LURK_Message_BenutzergruppeGeleert = CPH.GetGlobalVar<string>("LURK_Message_BenutzergruppeGeleert");
		string LURK_Message_FehlerBenutzergruppe = CPH.GetGlobalVar<string>("LURK_Message_FehlerBenutzergruppe");
		string LURK_Message_NiemandImLurk = CPH.GetGlobalVar<string>("LURK_Message_NiemandImLurk");

		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
		string currUserCommand = args.ContainsKey("command") ? args["command"].ToString() : "NoCommand";
		string currUserRawInput = args.ContainsKey("rawInput") ? args["rawInput"].ToString() : "NoRawInput";

		if ((currUser == BC_USER && currUserCommand.Contains(TriggerUserBC_Command)) ||
			(currUser == TriggerUser_1 && currUserRawInput.Contains(TriggerUserTEXT_1)) ||
			(currUser == TriggerUser_2 && currUserRawInput.Contains(TriggerUserTEXT_2)))

			{
				CPH.ClearUsersFromGroup(groupName);
				CPH.SendMessage(LURK_Message_meVoranstellen + " " + LURK_Message_BenutzergruppeGeleert);
				Thread.Sleep(200);

				var groupUsers = CPH.UsersInGroup(groupName);
				if (groupUsers == null)
				{
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + LURK_Message_FehlerBenutzergruppe);
					return false;
				}

				int userCount = groupUsers.Count;
				if (userCount == 0)
				{
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + LURK_Message_NiemandImLurk);
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
