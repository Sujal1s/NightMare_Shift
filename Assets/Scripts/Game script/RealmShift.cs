using System;
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
    }

  


    void ToggleRealmShift()
    {
        if (isRealmShifted == false)
        {
            
            isRealmShifted = true;
            Debug.Log("realm active");
            light.intensity = 0.21f;
            Debug.Log("intensity 0");
            spotlight.intensity = 0.61f;
        }
        else
        {
           
            isRealmShifted = false;
            light.intensity = 0.84f;
            spotlight.intensity = 0f;
            Debug.Log("realm deactive");
        }
    }
}