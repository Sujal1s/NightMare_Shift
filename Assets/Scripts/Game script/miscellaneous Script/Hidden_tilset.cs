using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Hidden_tilset : MonoBehaviour
{
    [SerializeField] GameObject hiddenTilemap;
    [SerializeField] GameObject prop;
    // Start is called before the first frame update
    void Start()
    {
        hiddenTilemap.SetActive(true);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            hiddenTilemap.SetActive(false);
            
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hiddenTilemap.SetActive(true);
        }
    }*/
}
