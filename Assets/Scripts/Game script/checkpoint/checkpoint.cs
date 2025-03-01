using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
   private Respwanscript respawn;

   private void Awake()
   {
      GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
      if (respawnObject != null)
      {
         respawn = respawnObject.GetComponent<Respwanscript>();
      }
      else
      {
         Debug.LogError("Respawn object not found!");
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player") && respawn != null)
      {
         respawn.respwanpoint = this.gameObject;
      }
   }
}