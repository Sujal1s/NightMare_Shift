using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achievement_prop : MonoBehaviour
{
    public bool playercollide = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playercollide = true;
            Debug.Log(playercollide);
        }
        
    }
}
