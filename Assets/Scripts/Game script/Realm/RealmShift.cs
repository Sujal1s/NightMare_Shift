using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RealmShift : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private Light2D spotlight;
    [SerializeField] private Volume volume;
    private Vignette vignette;

    public bool isRealmShifted = false;

    private void Awake()
    {
        volume.profile.TryGet(out vignette);
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5)) 
        {
            ToggleRealmShift();
        }
    }

    void ToggleRealmShift()
    {
        if (isRealmShifted == false)
        {
            Debug.Log("realm active");
            light.intensity = 0.5f;
            spotlight.intensity = 0.61f;
            vignette.intensity.value = 0.688f;
            isRealmShifted = true;
        }
        else
        {
            light.intensity = 0.84f;
            spotlight.intensity = 0f;   
            vignette.intensity.value = 0.382f;
            isRealmShifted = false;
            Debug.Log("realm deactive");
        }
    }
}