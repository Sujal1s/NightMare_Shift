using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class parallax : MonoBehaviour
    {
        public Camera cam;
        public Transform FollowTarget;
        float staringZ;
        float distanceformtarget => transform.position.z - FollowTarget.position.z;
        float parallaxFactor => Mathf.Abs(distanceformtarget);
        float clippingplane => (distanceformtarget > 0 ? cam.farClipPlane : cam.nearClipPlane);
    
        Vector2 camMoveSinceStart => (Vector2)cam.transform.position - new Vector2(0, staringZ);
        Vector2 Startingpos;
    
        private void Start()
        {
            Startingpos = transform.position;
            staringZ = transform.position.z;
        }
    
        private void Update()
        {
            if (parallaxFactor != 0)
            {
                Vector2 newPostion = Startingpos + camMoveSinceStart / parallaxFactor;
                transform.position = new Vector3(newPostion.x, newPostion.y, transform.position.z);
            }
        }
    }