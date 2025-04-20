using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RealmShift : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private Light2D spotlight;
    [SerializeField] private Volume volume;
    private Vignette vignette;
    [SerializeField] AudioSource audio;

    public bool isRealmShifted = false;

    private void Awake()
    {
        volume.profile.TryGet(out vignette);
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            
            StartCoroutine(RealmShifter());
            if (isRealmShifted == true)
            {
                audio.Play();
            }
            else
            {
                audio.Stop();   
            }
        }
    }
    

    void ToggleRealmShift()
    {
        if (isRealmShifted == false)
        {
            Debug.Log("realm active");
            light.intensity = 0.25f;
            spotlight.intensity = 0.61f;
            vignette.intensity.value = 0.688f;
            isRealmShifted = true;
            
        }
        else
        {
            light.intensity = 0.67f;
            spotlight.intensity = 0f;   
            vignette.intensity.value = 0.382f;
            isRealmShifted = false;
            Debug.Log("realm deactive");
        }
    }

    private IEnumerator RealmShifter()
    {
        ToggleRealmShift();
        yield return new WaitForSeconds(1f);
    }
    
}