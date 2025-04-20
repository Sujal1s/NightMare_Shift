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

    [SerializeField] Animator taskanimator;
    [Header("Task 1")]
    [SerializeField] TextMeshProUGUI Task1Text;
    [SerializeField] RawImage Task1image;
    [SerializeField] Shelf shelf;

    [Header("book ")]
    [SerializeField] TextMeshProUGUI bookPuzzleText;
    [SerializeField] RawImage Task2image;
    [SerializeField] Book book;

    [Header("shelf")]
    [SerializeField] TextMeshProUGUI shelfText;
    [SerializeField] RawImage Task3image;

    [SerializeField] GameObject Book;

    [SerializeField] private Animator _animator;

    void Start()
    {
        shelfText.enabled = false;
        Task3image.enabled = false;
        Book.SetActive(false);
    }

    void Update()
    {
        enddoor();
        bookpuzzle();
        shelfplace();
        bookcanvas();
    }

    void bookcanvas()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Book.SetActive(!Book.activeSelf);
            _animator.SetBool("isOpen", Book.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            
            Book.SetActive(false);
            _animator.SetBool("isOpen", false);
        }
 

    }

    void enddoor()
    {
        if (shelf.hasMoved)
        {
            Task1image.color = Color.green;
            Task1Text.text = "Found The End Door";
            Task1Text.color = Color.green;
        }
    }

    void bookpuzzle()
    {
        if (book.book > 0)
        {
            Task2image.color = Color.green;
            bookPuzzleText.color = Color.green;
            bookPuzzleText.text = "Book Collected";
            shelfText.enabled = true;
            Task3image.enabled = true;
        }
    }

    void shelfplace()
    {
        if (shelf.shouldMove)
        {
            shelfText.text = "Book Placed on Shelf";
            shelfText.color = Color.green;
            Task3image.color = Color.green;
        }
    }
}