using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialscript : MonoBehaviour
{
    
    [SerializeField] private GameObject tutorial;

    void Start()
    {
        tutorial.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            tutorial.SetActive(true);
            Debug.Log(tutorial);
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorial.SetActive(false);
          
        }
        
    
    }

  
}
