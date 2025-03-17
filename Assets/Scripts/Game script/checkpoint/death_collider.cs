using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_collider : MonoBehaviour
{
    [SerializeField] GameObject checkpoint;
    [SerializeField] GameObject keypoint;
    [SerializeField] Transform respawnPoint;
    [SerializeField] private checkpoint cp;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cp.checkpointrange == true)
        {
            other.transform.position = checkpoint.transform.position;
        }
        else
        {
            other.transform.position = respawnPoint.transform.position;
        }

        
    }
}
