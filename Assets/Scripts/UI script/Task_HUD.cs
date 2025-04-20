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

    [Header("Task 1")]
    [SerializeField] TextMeshProUGUI Task1Text;
    [SerializeField] RawImage Task1image;
    [SerializeField] Shelf shelf;

    [Header("book ")]
    [SerializeField] TextMeshProUGUI task2Text;
    [SerializeField] RawImage Task2image;
    [SerializeField] Book book;

    [Header("shelf")]
    [SerializeField] TextMeshProUGUI task3Text;
    [SerializeField] RawImage Task3image;

    [SerializeField] GameObject Book;

    [SerializeField] private Animator _animator;

    void Start()
    {
        Book.SetActive(false);
        taskhide();
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
            if (!Book.activeSelf)
            {
                // Opening the book
                Book.SetActive(true);
                _animator.SetBool("isOpen", true);
                _animator.SetBool("isClose", false);
                StartCoroutine(taskshow());
            }
            else
            {
                // Closing the book - start animation but delay hiding the object
                _animator.SetBool("isOpen", false);
                _animator.SetBool("isClose", true);
                StartCoroutine(DelayedHideBook());
                taskhide();
            }
        }
    }

    IEnumerator DelayedHideBook()
    {
        yield return new WaitForSeconds(0.8f);
        Book.SetActive(false);
    }

    IEnumerator taskshow()
    {
        yield return new WaitForSeconds(1.2f);
        Task1Text.enabled = true;
        Task1image.enabled = true;
        task2Text.enabled = true;
        Task2image.enabled = true;
        task3Text.enabled = true; 
        Task3image.enabled = true;
        
    }

    void taskhide()
    {
        Task1Text.enabled = false;
        Task1image.enabled = false;
        task2Text.enabled = false;
        Task2image.enabled = false;
        task3Text.enabled = false;
        Task3image.enabled = false;
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
            task2Text.color = Color.green;
            task2Text.text = "Book Collected";
        }
    }

    void shelfplace()
    {
        if (shelf.shouldMove)
        {
            task3Text.text = "Book Placed on Shelf";
            task3Text.color = Color.green;
            Task3image.color = Color.green;
        }
    }
}