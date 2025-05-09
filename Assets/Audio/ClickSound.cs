using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;  
    public AudioClip clickClip;     

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
    }
}
