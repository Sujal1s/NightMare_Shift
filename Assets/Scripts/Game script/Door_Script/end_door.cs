using System;
using UnityEngine;
using Cainos.LucidEditor;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif


namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class end_door : Scenetranstion // this is the end door that change the Scene
    {
      public SpriteRenderer spriteRenderer;
      public Sprite spriteOpened; 
      public Sprite spriteClosed;
      [SerializeField] Shelf shelf;


        private Animator Animator
        {
            get
            {
                if (animator == null ) animator = GetComponent<Animator>();
                return animator;
            }
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

        private void Start()
        {
            Animator.Play(isOpened ? "Opened" : "Closed");
            IsOpened = isOpened;
        }


        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Open")]
        public void Open()
        {
            IsOpened = true;
        }

        [FoldoutGroup("Runtime"), HorizontalGroup("Runtime/Button"), Button("Close")]
        public void Close()
        {
            IsOpened = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("enter");
            if ( other.CompareTag("Player") && shelf.hasMoved)
            {
                Open();
                fadein();
                StartCoroutine(ChangeSceneAfterDelay(2f));
            }
  
           
        }

        private IEnumerator ChangeSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("This is the last scene in build settings");
                
            }
        }
       
    }
}