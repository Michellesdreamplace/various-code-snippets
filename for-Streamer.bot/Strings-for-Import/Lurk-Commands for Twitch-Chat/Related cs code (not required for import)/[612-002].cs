// [612-002] ⫸  !lurk - !unlurk - !lurker
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
		int MaxMessageLength = CPH.GetGlobalVar<int>("LURK_MaxMessageLength");
		string LURK_Message_meVoranstellen = CPH.GetGlobalVar<string>("LURK_Message_meVoranstellen");
		string LURK_Message_wb = CPH.GetGlobalVar<string>("LURK_Message_wb");
		string LURK_Message_IstBereitsImLurk = CPH.GetGlobalVar<string>("LURK_Message_IstBereitsImLurk");
		string LURK_Message_GehtInLurk = CPH.GetGlobalVar<string>("LURK_Message_GehtInLurk");
		string LURK_Message_Unlurk = CPH.GetGlobalVar<string>("LURK_Message_Unlurk");
		string LURK_Message_UnlurkNoLurk = CPH.GetGlobalVar<string>("LURK_Message_UnlurkNoLurk");
		string LURK_Message_FehlerBenutzergruppe = CPH.GetGlobalVar<string>("LURK_Message_FehlerBenutzergruppe");
		string LURK_Message_NiemandImLurk = CPH.GetGlobalVar<string>("LURK_Message_NiemandImLurk");
		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
		string currUserCommand = args.ContainsKey("command") ? args["command"].ToString() : "NoCommand";

			if (currUserCommand == "!lurk")
			{
				if (CPH.UserInGroup(currUser, platform, groupName))
				{
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + currUser + " " + LURK_Message_IstBereitsImLurk);
				}
				else
				{
					CPH.AddUserToGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + currUser + " " + LURK_Message_GehtInLurk);
				}
			}

			else if (currUserCommand == "!unlurk")
			{
				if (CPH.UserInGroup(currUser, platform, groupName))
				{
					CPH.RemoveUserFromGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + LURK_Message_wb + " " + currUser + " " + LURK_Message_Unlurk);
				}
				else
				{
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + currUser + " " + LURK_Message_UnlurkNoLurk);
				}
			}

			else if (currUserCommand == "!lurker")
			{
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

				List<string> userNames = new List<string>();
				foreach (var user in groupUsers)
				{
					userNames.Add(user.Login);	// [ToDo]
				}

				string userNamesString = string.Join(", ", userNames);
				if (userNamesString.Length > MaxMessageLength)
				{
					List<string> chunks = new List<string>();
					string[] namesArray = userNamesString.Split(new string[] { ", " }, StringSplitOptions.None);
					string currentChunk = "";

					foreach (string name in namesArray)
					{
						if ((currentChunk + name + ", ").Length > MaxMessageLength)
						{
							chunks.Add(currentChunk.TrimEnd(',', ' '));
							currentChunk = "";
						}
						currentChunk += name + ", ";
					}

					if (currentChunk.Length > 0)
					{
						chunks.Add(currentChunk.TrimEnd(',', ' '));
					}

					for (int i = 0; i < chunks.Count; i++)
					{
						CPH.SendMessage(LURK_Message_meVoranstellen + " " + $"ℹ️ Aktuell im Lurk (Teil {i + 1}/{chunks.Count}): {chunks[i]} 💤");	// ◄◄◄◄◄ HIER kann ggf. der Text angepasst werden
						Thread.Sleep(200);
					}
				}
				else
				{
					CPH.SendMessage(LURK_Message_meVoranstellen + " " + "ℹ️ Aktuell (" + userCount + ") im Lurk: " + userNamesString + " 💤 Danke für den Support <3");	// ◄◄◄◄◄ HIER kann ggf. der Text angepasst werden
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
