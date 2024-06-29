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

		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ HIER kann der Text der Meldungen angepasst werden ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		int MaxMessageLength = 250;
		bool meVoranstellen = true;	// bei "true" wird ein "/me" bei allen Nachrichten vorangestellt - bei "false" nicht.

		string Message_wb = "wb ";	// Begrüßung, die bei UNLURK vor "currUser" vorangestellt wird
		string Message_GehtInLurk = " verschwindet in den Lurk! 💤 💤 💤 Danke für den Support <3";	// !lurk command ►►►["currUser" wird vorangestellt]
		string Message_IstBereitsImLurk = ", du bist doch schon längst im Lurk <3 Danke für den Support <3";	// erneuter !lurk command ►►►["currUser" wird vorangestellt]
		string Message_Unlurk = " <3 Schön, dass du wieder hier bist <3 <3 <3";	// !unlurk command ►►►["Message_wb"+"currUser" wird vorangestellt]
		string Message_UnlurkNoLurk = ", du warst doch gar nicht im Lurk <3";	// !unlurk command, obwohl gar nicht im Lurk war ►►►["currUser" wird vorangestellt]
		string Message_UnlurkPerText = " <3 Schön, dass du wieder aus dem Lurk zurück bist <3 <3 <3";	// Lurker, die irgendwas schreiben -> Unlurk ►►►["Message_wb"+"currUser" wird vorangestellt]
		string Message_NiemandImLurk = "ℹ️ Derzeit ist niemand im Lurk 💤";	// Meldung, wenn Niemand im Lurk ist

		string Message_BenutzergruppeGeleert = "/me Gruppe der Lurker geleert...";	// Meldung, wenn die Lurker-Gruppe geleert wurde
		string Message_FehlerBenutzergruppe = "/me Fehler: Konnte die Benutzergruppe nicht abrufen.";	// Fehlermeldung


		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ Benutzer und deren Texte, zum leeren der Lurker-Gruppe ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		string LURK_TriggerUser_1 = "Sery_Bot";	// Benutzername, der den Trigger zum Gruppe-leeren auslöst.
		string LURK_TriggerUserTEXT_1 = "Sery_Bot has joined";	// Text, den der Benutzer schreibt, damit die Gruppe geleert wird.
		string LURK_TriggerUser_2 = "Michelle_7b7";	// 2. Benutzername, der den Trigger zum Gruppe-leeren auslöst.
		string LURK_TriggerUserTEXT_2 = "Lurker Gruppe leeren";	// 2. Text, den der Benutzer schreibt, damit die Gruppe geleert wird.


//------------------------------------------------------------------------------------------------------------------------------------------------------
// 		ab hier bitte NICHTS ändern:
//------------------------------------------------------------------------------------------------------------------------------------------------------

		//▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ Variablen erstellen ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
		CPH.SetGlobalVar("LURK_MaxMessageLength", MaxMessageLength, true);
		CPH.SetGlobalVar("LURK_meVoranstellen", meVoranstellen, true);
		CPH.SetGlobalVar("LURK_Message_wb", Message_wb, true);
		CPH.SetGlobalVar("LURK_Message_GehtInLurk", Message_GehtInLurk, true);
		CPH.SetGlobalVar("LURK_Message_IstBereitsImLurk", Message_IstBereitsImLurk, true);
		CPH.SetGlobalVar("LURK_Message_Unlurk", Message_Unlurk, true);
		CPH.SetGlobalVar("LURK_Message_UnlurkNoLurk", Message_UnlurkNoLurk, true);
		CPH.SetGlobalVar("LURK_Message_UnlurkPerText", Message_UnlurkPerText, true);
		CPH.SetGlobalVar("LURK_Message_NiemandImLurk", Message_NiemandImLurk, true);
		CPH.SetGlobalVar("LURK_Message_BenutzergruppeGeleert", Message_BenutzergruppeGeleert, true);
		CPH.SetGlobalVar("LURK_Message_FehlerBenutzergruppe", Message_FehlerBenutzergruppe, true);
		CPH.SetGlobalVar("LURK_TriggerUser_1", LURK_TriggerUser_1, true);
		CPH.SetGlobalVar("LURK_TriggerUserTEXT_1", LURK_TriggerUserTEXT_1, true);
		CPH.SetGlobalVar("LURK_TriggerUser_2", LURK_TriggerUser_2, true);
		CPH.SetGlobalVar("LURK_TriggerUserTEXT_2", LURK_TriggerUserTEXT_2, true);
		
		bool LURK_meVoranstellen = CPH.GetGlobalVar<bool>("LURK_meVoranstellen");
		if (LURK_meVoranstellen)	{
			CPH.SetGlobalVar("LURK_Message_meVoranstellen", "/me", true);
			}	else	{
			CPH.SetGlobalVar("LURK_Message_meVoranstellen", "", true);
			}

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
		Thread.Sleep(200);
		CPH.AddUserToGroup(currBroadcasterUser, platform, groupName);
		Thread.Sleep(200);
		CPH.RemoveUserFromGroup(currBroadcasterUser, platform, groupName);
		Thread.Sleep(200);

	return true;
	}
}
