using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_camera : MonoBehaviour
{
    public Transform player; 

   
    void Update()
    {
        if (player != null)
        {
            
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
}