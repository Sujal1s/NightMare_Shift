using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour // this will use for to  store the currnt level , transfrom of player and achivement
{
    public int  level  = 0;
    public int achivement = 0;

    public void savePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        achivement = data.achivement;

        Vector3 postion;
        postion.x = data.postion[0];
        postion.y = data.postion[1];
        postion.z = data.postion[2];
        transform.position = postion;
        
        

    }
}
