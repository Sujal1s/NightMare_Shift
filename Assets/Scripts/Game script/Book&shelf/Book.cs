using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{
    public int book;

    [SerializeField] private TextMeshProUGUI bookcollected;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            book++;
            Debug.Log("book collected");
            bookcollected.text = "You Collected the book";
        }
    }
}
