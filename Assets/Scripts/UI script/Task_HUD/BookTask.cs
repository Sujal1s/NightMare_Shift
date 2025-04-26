using UnityEngine;

public class BookTask : BaseTask
{
    [SerializeField] private Book book;

    private void Start()
    {
        Initialize("Collect Book");
    }

    public override bool CheckCompletionCondition()
    {
        if (book.book > 0)
        {
            taskText.text = "Book Collected";
            return true;
        }
        return false;
    }
}