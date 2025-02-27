using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Switch : MonoBehaviour
    {
         public Door target; // Door is as script refreance and target is for the door
        
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
            
            if (Input.GetKeyDown((KeyCode.E)) && range)
            {
                TurnOn();
                target.Open();
                Debug.Log("on");
            }
            else
            {
                if (Input.GetKeyDown((KeyCode.E)) && range)
                {
                    TurnOff();
                    target.Close();
                    Debug.Log("off");
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

                #if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    EditorUtility.SetDirty(this);
                    EditorSceneManager.MarkSceneDirty(gameObject.scene);
                }
                #endif

                if (target) target.IsOpened = isOn;

                if (Application.isPlaying )
                {
                    Animator.SetBool("IsOn", isOn);
                }
                else
                {
                    if (spriteRenderer) spriteRenderer.sprite = isOn ? spriteOn: spriteOff;
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
