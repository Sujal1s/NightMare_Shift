using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialscript : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    public Animator animator;
    private bool hasTriggered = false;

    void Start()
    {
        tutorial.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            tutorial.SetActive(true);
            animator.SetTrigger("OpenTrigger");
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorial.SetActive(false);
            hasTriggered = false;
        }
    }
}