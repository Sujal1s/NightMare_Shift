using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickClip;
    public AudioSource soundPlayer;
    void Start()
    {

    }
    void Update()
    {

    }
    public void PlayThisSoundEffect()
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned!");
        }
        soundPlayer.Play();
    }
}