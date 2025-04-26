using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keytask : BaseTask
{
    [SerializeField] public key k;
    private    void Start()
    {
        k = GetComponent<key>();
        Initialize("Collect with key");
    }

    void Update()
    {

    }

    public override bool CheckCompletionCondition()
    {
        if ( k.doorkey > 0)
        {
           
            taskText.text = "key collected";
            return true;
        }

        
        return false;
    }

}