using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    
    public class achievement_script : MonoBehaviour
    {
        private float achievement_Collected;
        [SerializeField] GameObject achievement_prop;
        [SerializeField] Canvas canvas;
        private achievement_prop ap;
        [SerializeField] TextMeshProUGUI achievement_text;
    
        void Start()
        {
            canvas.gameObject.SetActive(false);
            
            // Initialize the ap variable
            if (achievement_prop != null)
            {
                ap = achievement_prop.GetComponent<achievement_prop>();
            }
            else
            {
                Debug.LogError("achievement_prop GameObject is not assigned in the inspector");
            }
        }
    
        private void Update()
        {
            collect();
        }
    
        void collect()
        {
            if (ap != null && ap.playercollide == true)
            {
                achievement_Collected++;
                canvas.gameObject.SetActive(true);
                Destroy(ap.gameObject);
                achievement_text.text = "You Collected the achievement" + achievement_Collected;
                Debug.Log(ap.playercollide);
                Debug.Log(achievement_Collected);
            }
        }
    }