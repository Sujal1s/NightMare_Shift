using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RealmShift : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private Light2D spotlight;
    [SerializeField] private Volume volume;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float duration = 3f;
    private Vignette vignette;
    [SerializeField] AudioSource audio;

    public bool isRealmShifted = false;
    private bool isOnCooldown = false;
    private float cooldownTimeRemaining = 0f;

    // Visual state values
    private readonly float shiftedLightIntensity = 0.25f;
    private readonly float shiftedSpotlightIntensity = 0.61f;
    private readonly float shiftedVignetteIntensity = 0.688f;
    
    private readonly float normalLightIntensity = 0.67f;
    private readonly float normalSpotlightIntensity = 0f;
    private readonly float normalVignetteIntensity = 0.382f;

    private void Awake()
    {
        volume.profile.TryGet(out vignette);
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Update cooldown timer
        if (isOnCooldown)
        {
            cooldownTimeRemaining -= Time.deltaTime;
            if (cooldownTimeRemaining <= 0f)
            {
                isOnCooldown = false;
            }
        }

        // Check for realm shift activation
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5)) && !isOnCooldown && !isRealmShifted)
        {
            StartCoroutine(RealmShifter());
            audio.Play();
        }
    }

    void ToggleRealmShift()
    {
        if (isRealmShifted == false)
        {
            Debug.Log("realm active");
            light.intensity = shiftedLightIntensity;
            spotlight.intensity = shiftedSpotlightIntensity;
            vignette.intensity.value = shiftedVignetteIntensity;
            isRealmShifted = true;
        }
        else
        {
            light.intensity = normalLightIntensity;
            spotlight.intensity = normalSpotlightIntensity;
            vignette.intensity.value = normalVignetteIntensity;
            isRealmShifted = false;
            Debug.Log("realm deactive");
            audio.Stop();
        }
    }

    private IEnumerator RealmShifter()
    {
        ToggleRealmShift();

        yield return new WaitForSeconds(duration - 1f);

        if (isRealmShifted)
        {
            yield return StartCoroutine(FlickerWarning());
        }

        if (isRealmShifted)
        {
            ToggleRealmShift();
        }

        isOnCooldown = true;
        cooldownTimeRemaining = cooldown;
        Debug.Log($"Realm shift on cooldown for {cooldown} seconds");
    }

    private IEnumerator FlickerWarning()
    {
        float flickerInterval = 0.15f; 
        float totalTime = 0f;
        bool isInNormalState = false;

        while (totalTime < 1f)
        {
            if (isInNormalState)
            {
                light.intensity = shiftedLightIntensity;
                spotlight.intensity = shiftedSpotlightIntensity;
                vignette.intensity.value = shiftedVignetteIntensity;
            }
            else
            {
                light.intensity = normalLightIntensity;
                spotlight.intensity = normalSpotlightIntensity;
                vignette.intensity.value = normalVignetteIntensity;
            }

            isInNormalState = !isInNormalState;
            yield return new WaitForSeconds(flickerInterval);
            totalTime += flickerInterval;
        }
        
        light.intensity = shiftedLightIntensity;
        spotlight.intensity = shiftedSpotlightIntensity;
        vignette.intensity.value = shiftedVignetteIntensity;
    }
}