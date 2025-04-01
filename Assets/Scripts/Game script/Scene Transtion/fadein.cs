using System;
using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtPlatformer_Dungeon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadein : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float FadeDealy = 0.5f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Fade());
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }



    IEnumerator Fade()
    {
        yield return new WaitForSeconds(FadeDealy);
        animator.SetBool("On", true);
        
    }
    
}
