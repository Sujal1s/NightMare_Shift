
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task_HUD : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform Playertransfomation; 
    
    [Header("End Door")]
    [SerializeField] TextMeshProUGUI endDoorText;
    [SerializeField] RawImage endDoorimage; 
    [SerializeField] Transform endDoor;
    [SerializeField] Shelf shelf;
    
    [Header("book ")]
    [SerializeField] TextMeshProUGUI bookPuzzleText;
    [SerializeField] RawImage RawImagebookPuzzle;
    [SerializeField] Book book;
    
    [Header("shelf")]
    [SerializeField] TextMeshProUGUI shelfText;
    [SerializeField] RawImage RawImageshelf;

    void Start()
    {
        shelfText.enabled = false;
        RawImageshelf.enabled = false;
    }

    void Update()
    {
        enddoor();
        bookpuzzle();
        shelfplace();
    }

    void enddoor()
    {
        if (shelf.hasMoved)
        {
            endDoorimage.color = Color.green;
            endDoorText.text = "Found The End Door";
            endDoorText.color = Color.green;
        }
    }

    void bookpuzzle()
    {
        if (book.book > 0)
        {
            RawImagebookPuzzle.color = Color.green;
            bookPuzzleText.color = Color.green;
            bookPuzzleText.text = "Book Collected";
            // shelf task enable after book collection
            shelfText.enabled = true;
            RawImageshelf.enabled = true;
        }
    }

    void shelfplace()
    {
        if (shelf.shouldMove)
        {
            shelfText.text = "Book Placed on Shelf";
            shelfText.color = Color.green;
            RawImageshelf.color = Color.green;
        }
    }
}