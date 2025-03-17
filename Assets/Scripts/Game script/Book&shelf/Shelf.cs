using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private GameObject ShelfBook;
    [SerializeField] private Book Target;
    [SerializeField] private GameObject shelf;
    [SerializeField] private float speed = 3f;
    private Vector3 targetPosition = new Vector3(209.56f, -4.905f, 0);
    private bool hasMoved = false;
    private bool shouldMove = false;

    void Start()
    {
        ShelfBook.SetActive(false);
    }

    void Update()
    {
        if (shouldMove && !hasMoved)
        {
            StartCoroutine(DelayedShelfCheck());
        }
    }

    IEnumerator DelayedShelfCheck()
    {
        yield return new WaitForSeconds(2f);
        shelfcheck();
    }

    void shelfcheck()
    {
        if (Target.book == 1)
        {
            shelf.transform.position = Vector3.MoveTowards(shelf.transform.position, targetPosition, speed * Time.deltaTime);
            if (shelf.transform.position == targetPosition)
            {
                hasMoved = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Target.book == 1)
        {
            ShelfBook.SetActive(true);
            shouldMove = true;
            
            Debug.Log(Target.book);
        }
    }
}