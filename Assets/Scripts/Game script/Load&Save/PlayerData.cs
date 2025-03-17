using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
    public int  level ;
    public int achivement;
    public float[] postion;

    public PlayerData(Player player)
    {
        level = player.level;
        achivement = player.achivement;
        
        postion = new float[3];
        postion[0] = player.transform.position.x;
        postion[1] = player.transform.position.y;
        postion[2] = player.transform.position.z;

    }
 
}
