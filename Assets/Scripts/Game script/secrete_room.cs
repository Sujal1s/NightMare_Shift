using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class secrete_room : MonoBehaviour
{
 [SerializeField] private GameObject tilset;
 [SerializeField] private Light2D light; // globle light set to zero
 [SerializeField] private List<GameObject> lighton; // light those need to turn on in room
 [SerializeField] private List<GameObject> lightoff; // sroundind light to turn off 


 void Start()
 {

  foreach (GameObject obj in lighton)
  {
   obj.SetActive(false);
  }

 }

 void update()
 {

 }

 private void OnTriggerEnter2D(Collider2D other)
 {
  light.intensity = 0.0f;
  tilset.SetActive(false);
  foreach (GameObject obj in lighton)
  {
   obj.SetActive(true);
  }

  foreach (GameObject obj in lightoff)
  {
   obj.SetActive(false);
  }
 }

 private void OnTriggerExit2D(Collider2D other)
 {
  light.intensity = 0.84f;
  tilset.SetActive(true);
  foreach (GameObject obj in lighton)
  {
   obj.SetActive(false);
  }

  foreach (GameObject obj in lightoff)
  {
   obj.SetActive(true);
  }
 }
}
 
