using UnityEngine;

public class ShelfTask : BaseTask
{
    [SerializeField] private Shelf shelf;

    private void Start()
    {
        Initialize("Place Book on Shelf");
    }

    public override bool CheckCompletionCondition()
    {
        if (shelf.shouldMove)
        {
            taskText.text = "Book Placed on Shelf";
            return true;
        }
        return false;
    }
}