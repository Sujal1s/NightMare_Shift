using UnityEngine;

public class EndDoorTask : BaseTask
{
    [SerializeField] private Shelf shelf;

    private void Start()
    {
        Initialize("Find The End Door");
    }

    public override bool CheckCompletionCondition()
    {
        if (shelf.hasMoved)
        {
            taskText.text = "Found The End Door";
            return true;
        }
        return false;
    }
}