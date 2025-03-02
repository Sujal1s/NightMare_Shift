using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
   private Respwanscript respawn;
   private GameObject lit;

   private void Awake()
   {
      lit = GameObject.FindWithTag("lit");
      if (lit != null)
      {
         Debug.Log("found lit");
      }
      else
      {
         Debug.Log("no lit");
      }

      lit.SetActive(false);   
      
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
         respawn.respwanpoint = gameObject;
         StartCoroutine(checkpoint_lit());

      }
   }

   IEnumerator checkpoint_lit()
   {
      yield return new WaitForSeconds(1f);
      lit.SetActive(true);
   }
}