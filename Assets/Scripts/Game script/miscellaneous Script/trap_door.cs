using System;
using System.Collections;
using UnityEngine;
using Cainos.LucidEditor;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif


namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class trap_door : MonoBehaviour // this script is used on door that required lever
    {
        
        [FoldoutGroup("Reference")] public SpriteRenderer spriteRenderer;
        [FoldoutGroup("Reference")] public Sprite spriteOpened;
        [FoldoutGroup("Reference")] public Sprite spriteClosed;


        private Animator Animator
        {
            get
            {
                if (animator == null ) animator = GetComponent<Animator>();
                return animator;
            }
        }

        private void Awake()
        {

        }

        private Animator animator;
        

        [FoldoutGroup("Runtime"), ShowInInspector]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;

                #if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    EditorUtility.SetDirty(this);
                    EditorSceneManager.MarkSceneDirty(gameObject.scene);
                }
                #endif


                if (Application.isPlaying)
                {
                    Animator.SetBool("IsOpened", isOpened);
                }
                else
                {
                    if(spriteRenderer) spriteRenderer.sprite = isOpened ? spriteOpened : spriteClosed;
                }
            }
        }
        [SerializeField,HideInInspector]
        private bool isOpened;

        private void Update()
        {
            
        }

        private void Start()
        {
            Animator.Play(isOpened ? "Opened" : "Closed");
            IsOpened = isOpened;
        }


        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Open")]
        public void Open()
        {
            IsOpened = true;
            if ( isOpened )
            {
             
            }
            
        }

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Close")]
        public void Close()
        {
            IsOpened = false;
            if (isOpened == false)
            {
               
            }
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(opentarp());
            }
            
        }

        void OnCollisionExit2D(Collision2D other)
        {
            StartCoroutine(Closetrap());
        }
        private IEnumerator opentarp()
        {
            Open();
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator Closetrap()
        {
            Close();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
