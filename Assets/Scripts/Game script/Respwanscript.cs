using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwanscript : MonoBehaviour
{
    public GameObject player;
    public GameObject respwanpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = respwanpoint.transform.position;
        }
    }
}
