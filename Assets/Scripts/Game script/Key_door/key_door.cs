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
    public class Key_door : MonoBehaviour // this script is used on door that required lever
    {
        [SerializeField] private key key;
        [SerializeField] private GameObject door_key;
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
            cd = GetComponent<BoxCollider2D>();
            door_key.SetActive(false);
           
            if (cd  != null)
            {
                Debug.Log("cd enable");
                
            }
        }

        private Animator animator;
        private BoxCollider2D cd;

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
                cd.isTrigger = true;
            }
            
        }

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Close")]
        public void Close()
        {
            IsOpened = false;
            if (isOpened == false)
            {
                cd.isTrigger = false;
            }
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player") && key.doorkey == 1 )
            {
                
                Destroy(key.gameObject);
                StartCoroutine(Keyactive());
                StartCoroutine(destroydoorkey());
                StartCoroutine(destroykey());

            }
            else
            {
                Close();

            }

            IEnumerator destroydoorkey()
            {
                yield return new WaitForSeconds(1f);
                Destroy(door_key.gameObject);
            }
            IEnumerator Keyactive()
            {
                yield return new WaitForSeconds(0.5f);
                door_key.SetActive(true);
            }
            IEnumerator destroykey()
            {
                yield return new WaitForSeconds(1f);
                Open();
            }
        }
    }
}
