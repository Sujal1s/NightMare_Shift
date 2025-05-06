using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    
    GameObject player;
    private Vector2 vel;
    public bool ispickup;
    [SerializeField] private float smoothTime = 0.3f;
     public int doorkey = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (ispickup)
        {
            Vector3 offset = new Vector3(1 , -1 , 0) ;
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !ispickup)
        {
            doorkey++;
            Debug.Log(doorkey);
            ispickup = true;
        }
    }
}
