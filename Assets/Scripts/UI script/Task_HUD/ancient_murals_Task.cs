using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ancient_murals_Task : BaseTask
{
    // Start is called before the first frame update
    [SerializeField] private ancient_murals murals;
    private void Start()
    {
        murals = GetComponent<ancient_murals>();
        Initialize("Find The Ancient Mural");
    }

    public override bool CheckCompletionCondition()
    {
        if (murals.isNear)
        {
            taskText.text = "Found The blank  history";
            return true;
        }
        return false;
    }
}
