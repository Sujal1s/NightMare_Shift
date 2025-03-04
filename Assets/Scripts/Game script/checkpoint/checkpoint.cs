using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
   [SerializeField] private GameObject lit;

   public bool checkpointrange = false;

   private void Awake()
   {
      
     
      lit.SetActive(false);
     
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      checkpointrange = true;
      StartCoroutine(checkpoint_lit());
   }

   IEnumerator checkpoint_lit()
   {
      yield return new WaitForSeconds(1f);
     
     
         lit.SetActive(true);
        
   
     
      
   }
}