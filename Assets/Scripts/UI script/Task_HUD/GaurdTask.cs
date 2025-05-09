using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdTask : BaseTask
{
    [SerializeField]private DialogueSystem DS;
    private    void Start()
    {
        DS = FindObjectOfType<DialogueSystem>();
        Initialize("Meet with Gaurd");
    }

    void Update()
    {

    }

    public override bool CheckCompletionCondition()
    {
        if (DS.dialogueActive)
        {
            taskText.text = "Gaurd Met";
            return true;
        }

        
        return false;
    }

}
