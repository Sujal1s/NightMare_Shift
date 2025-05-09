using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenetranstion : MonoBehaviour
{

    void Start()
    {
        _ani = GetComponent<Animator>();
    }
      public string Scenename;
      [SerializeField] private Animator _ani;

   public   void fadein()
      {
          if (CompareTag("Player"))
          {
              SceneManager.LoadScene(Scenename);
          }
          _ani.SetBool("isfade", true);
      }
      
      
    
}
