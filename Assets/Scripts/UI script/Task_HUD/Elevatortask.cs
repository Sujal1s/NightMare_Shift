using UnityEngine;

public class Elevatortask : BaseTask
{
    [SerializeField] private Shelf shelf;

    private void Start()
    {
        Initialize("check out the elevator");
    }

    public override bool CheckCompletionCondition()
    {
        if (shelf.shouldMove)
        {
            taskText.text = "elevator Ride done";
            return true;
        }
        return false;
    }
}