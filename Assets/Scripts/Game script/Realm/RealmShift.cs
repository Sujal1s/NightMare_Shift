using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.Rendering.Universal;
public class RealmShift : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private Light2D spotlight;
    public bool isRealmShifted = false;


    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)|| Input.GetKeyDown(KeyCode.JoystickButton5)) 
        {
            
            ToggleRealmShift();
        }

        if (isRealmShifted == true)
        {
          
        }
        else if (isRealmShifted == false)
        {
            
        }
        {
            
        }
    }

  


    void ToggleRealmShift()
    {
        if (isRealmShifted == false)
        {
            
            
            Debug.Log("realm active");
            light.intensity = 0.21f;
            Debug.Log("intensity 0");
            spotlight.intensity = 0.61f;
            isRealmShifted = true;
            
            
        }
        else
        {
           
            
            light.intensity = 0.84f;
            spotlight.intensity = 0f;
            isRealmShifted = false;
            Debug.Log("realm deactive");
        }
    }
}