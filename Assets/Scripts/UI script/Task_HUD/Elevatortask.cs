using System;
using UnityEngine;

public class Elevatortask : BaseTask
{

    private bool issaw;
    private void Start()
    {
        issaw = false;
        Initialize("check out the elevator");
    }

    public override bool CheckCompletionCondition()
    {
        if (issaw)
        {
            taskText.text = "elevator Check out";
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        issaw = true;
    }
}