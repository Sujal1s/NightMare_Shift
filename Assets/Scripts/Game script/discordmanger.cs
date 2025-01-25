using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discordmanger : MonoBehaviour
{
     Discord.Discord discord;
    
    void Start()
    {
        discord = new Discord.Discord(1332693109875802132, (ulong)Discord.CreateFlags.NoRequireDiscord);
        ChangeActivity();
    }

    private void OnDisable()
    {
        discord.Dispose();
        
    }

    private void ChangeActivity()
    {
        var activitymanger = discord.GetActivityManager();
        var activty = new Discord.Activity
        {
           
            State = "At School",
            Details = "Exploring dark world",
            Timestamps =
            {
                Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
               
            }
        };
        activitymanger.UpdateActivity(activty, (res) => { Debug.Log("activty updated"); });
        
    }
 
    void Update()
    {
        discord.RunCallbacks();
    }
}