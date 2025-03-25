using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour
{
    public int book;
    [SerializeField] private TextMeshProUGUI bookcollected;
    private bool collsion = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3)  && collsion || Input.GetKeyDown(KeyCode.E)  && collsion)
        {
            book++;
            Debug.Log(book);
            bookcollected.text = book.ToString();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collsion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collsion = false;
        }
    }
}