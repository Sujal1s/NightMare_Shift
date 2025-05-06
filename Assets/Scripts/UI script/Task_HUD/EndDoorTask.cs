using UnityEngine;

public class EndDoorTask : BaseTask
{
    [SerializeField] private Shelf shelf;
    bool isrange = false;

    private void Start()
    {
        Initialize("Find The End Door");
    }

    public override bool CheckCompletionCondition()
    {
        if (shelf != null && shelf.hasMoved)
        {
            taskText.text = "Found The End Door";
            return true;
        }
        else if (isrange)
        {
            taskText.text = "Found The End Door";
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isrange = true;
    }

 
}