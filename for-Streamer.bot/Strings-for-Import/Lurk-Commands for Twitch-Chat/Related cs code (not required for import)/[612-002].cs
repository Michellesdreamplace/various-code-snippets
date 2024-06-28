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
		int MaxMessageLength = 250;
		Platform platform = Platform.Twitch;
		string groupName = CPH.GetGlobalVar<string>("GROUP_NAME_Current_Lurker");
		string currUser = args.ContainsKey("user") ? args["user"].ToString() : "UnknownUser";
		string currUserCommand = args.ContainsKey("command") ? args["command"].ToString() : "NoCommand";

			if (currUserCommand == "!lurk")
			{
				if (CPH.UserInGroup(currUser, platform, groupName))
				{
					CPH.SendMessage(currUser + ", du bist doch schon längst im Lurk <3 Danke für den Support <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
					//return true;
				}
				else
				{
					CPH.AddUserToGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage(currUser + " verschwindet in den Lurk! 💤 💤 💤 Danke für den Support <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
					//return true;
				}
			}

			else if (currUserCommand == "!unlurk")
			{
				if (CPH.UserInGroup(currUser, platform, groupName))
				{
					CPH.RemoveUserFromGroup(currUser, platform, groupName);
					Thread.Sleep(200);
					CPH.SendMessage("wb " + currUser + " <3 Schön, dass du wieder hier bist <3 <3 <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
					//return true;
				}
				else
				{
					CPH.SendMessage(currUser + ", du warst doch gar nicht im Lurk <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
					//return true;
				}
			}

			else if (currUserCommand == "!lurker")
			{
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
					//return true;
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
						CPH.SendMessage($"ℹ️ Aktuell im Lurk (Teil {i + 1}/{chunks.Count}): {chunks[i]} 💤");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
						Thread.Sleep(200);
					}
				}
				else
				{
					CPH.SendMessage("ℹ️ Aktuell (" + userCount + ") im Lurk: " + userNamesString + " 💤 Danke für den Support <3");	// ◄◄◄◄◄ HIER kann der Text angepasst werden
				}
				//return true;
			}

		// Count in .txt schreiben
		string savePath = CPH.GetGlobalVar<string>("TXT_for_OBS_Pfad");
		string saveName = CPH.GetGlobalVar<string>("TXT_Filename_Current_Lurker_Count");
		string saveFile = (savePath + saveName);
		var groupUsersCount = CPH.UsersInGroup(groupName);
		int userCount_int = groupUsersCount.Count;
		string userCount_String = userCount_int.ToString(); // Neue Zeile: Umwandlung von int zu string
		File.WriteAllText(saveFile, userCount_String);

		return true;
    }
}
