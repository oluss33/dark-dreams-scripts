using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour {

	public Discord.Discord discord;

	public string details;
	public string state;
	public string largeText;
	private long time;

	// Use this for initialization
	void Start () {
		discord = new Discord.Discord(849334387493699626, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
		time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
		UpdateStatus();
	}

	void Update()
	{
		try
		{
			discord.RunCallbacks();
		}
		catch
		{
			Destroy(gameObject);
		}
	}

	void LateUpdate()
	{
		UpdateStatus();
	}

	void UpdateStatus()
	{
		try
		{
			var activityManager = discord.GetActivityManager();
			var activity = new Discord.Activity
			{
				Details = details,
				State = state,
				Assets =
				{
					LargeImage =  "dd",
					LargeText = largeText
				},
				Timestamps =
				{
					Start = time
				}
			};
			activityManager.UpdateActivity(activity, (res) =>
			{
				if (res != Discord.Result.Ok)
				{
					Debug.LogError("Failed to connect with discord!");
				}
			});
		}
		catch
		{
			Destroy(gameObject);
		}
	}
	
	private void OnDestroy()
	{
		try
		{
			discord.Dispose();
		}
		catch
		{
			Debug.Log("COS JEBLO");
		}
	}
/*	
	void Update () {
		if (discord != null)
		{
			discord.RunCallbacks();
		}
	}

    private void OnDisable()
    {
		discord.Dispose();
    }

    private void OnDestroy()
    {
	    discord.Dispose();
    }
    */
}