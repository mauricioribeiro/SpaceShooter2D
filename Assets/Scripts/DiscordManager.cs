using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordManager : MonoBehaviour {

	private Discord.Discord _discord;

	private Discord.ActivityManager _discordActivity; 

	readonly long CLIENT_ID = 693796291183902750;

	void Start () {
		_discord = new Discord.Discord(CLIENT_ID, (UInt64)Discord.CreateFlags.Default);
		_discordActivity = _discord.GetActivityManager();
		UpdateActivity(GetDefaultActivity());
	}
	
	void Update () {
		if (_discord != null)
		{
			_discord.RunCallbacks();
		}
	}

	void OnApplicationQuit () {
		_discordActivity.ClearActivity((res) => {
			if (res == Discord.Result.Ok)
			{
				Debug.Log("Discord cleared activity successfully");
			} else {
				Debug.LogWarning("Discord activity not cleared");
			}
		});
	}

	public void UpdateActivity (Activity activity) {
		_discordActivity.UpdateActivity(activity, (res) =>
		{
			if (res == Discord.Result.Ok)
			{
				Debug.Log("Discord updated activity successfully");
			} else {
				Debug.LogWarning("Failed to update Discord activity");
			}
		});
	}

	public Activity GetDefaultActivity () {
		return new Discord.Activity
		{
			State = "Playing single player",
			Secrets =
			{
				Join = "jMTIzNDV8MTIzNDV8MTMyNDU0",
				Spectate = "sMTIzNDV8MTIzNDV8MTMyNDU0",
			}
		};
	}

	public Activity GetPowerUpActivity (string id) {
		return new Discord.Activity
		{
			State = "Got " + id + " power up",
			Secrets =
			{
				Join = "jMTIzNDV8MTIzNDV8MTMyNDU0",
				Spectate = "sMTIzNDV8MTIzNDV8MTMyNDU0",
			}
		};
	}
}