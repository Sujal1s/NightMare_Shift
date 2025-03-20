using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using UnityEditor;


namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Switch_pully : MonoBehaviour // this script is used for the lever who help the door to unlock
    {
        public Elevator target; // Reference to the elevator to control

        public SpriteRenderer spriteRenderer;
        public Sprite spriteOn;
        public Sprite spriteOff;
        private bool range;

        private Animator Animator
        {
            get
            {
                if (animator == null) animator = GetComponent<Animator>();
                return animator;
            }
        }
        private Animator animator;

        private void Update()
        {
            if (Input.GetKeyDown((KeyCode.E)) && range || (Input.GetKeyDown(KeyCode.JoystickButton3)) && range )
            {
                if (isOn)
                {
                    TurnOff();
                    Debug.Log("off");
                }
                else
                {
                    TurnOn();
                    Debug.Log("on");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                range = true;
                Debug.Log("enter the zone");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            range = false;
            Debug.Log("exit the zone");
        }

        private void Start()
        {
            Animator.SetBool("IsOn", isOn);
            IsOn = isOn;
        }

        [FoldoutGroup("Runtime"), ShowInInspector]
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;

                // Control the elevator based on switch state
                if (target) 
                {
                    target.SetActiveState(isOn);
                }

                if (Application.isPlaying)
                {
                    Animator.SetBool("IsOn", isOn);
                }
                else
                {
                    if (spriteRenderer) spriteRenderer.sprite = isOn ? spriteOn : spriteOff;
                }
            }
        }
        [SerializeField, HideInInspector]
        private bool isOn;

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Turn On")]
        public void TurnOn()
        {
            IsOn = true;
        }

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Turn Off")]
        public void TurnOff()
        {
            IsOn = false;
        }
    }
}