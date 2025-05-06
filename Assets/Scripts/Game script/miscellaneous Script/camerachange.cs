using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerachange : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] Camera maincamera;
    [SerializeField] Camera secondcamera;
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
           maincamera.GetComponent<Camera>().enabled = false;
              secondcamera.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            maincamera.GetComponent<Camera>().enabled = true;
            secondcamera.GetComponent<Camera>().enabled = false;
        }
    }
}
