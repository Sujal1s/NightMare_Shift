using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTask : MonoBehaviour
{
    [Header("Task UI")]
    [SerializeField] protected TextMeshProUGUI taskText;
    [SerializeField] protected RawImage taskIcon;
    [SerializeField] protected string taskTitle;
    
    protected bool isCompleted = false;
    protected bool isActive = true;

    public virtual void Initialize(string title)
    {
        taskTitle = title;
        taskText.text = title;
        ResetTaskUI();
    }

    public virtual void CompleteTask()
    {
        isCompleted = true;
        taskText.color = Color.green;
        taskIcon.color = Color.green;
    }

    public virtual void ResetTaskUI()
    {
        isCompleted = false;
        taskText.color = Color.yellow;
        taskIcon.color = Color.yellow;
    }

    public virtual void SetActive(bool active)
    {
        isActive = active;
        taskText.enabled = active;
        taskIcon.enabled = active;
    }

    public bool IsCompleted() => isCompleted;
    public bool IsActive() => isActive;
    
    public abstract bool CheckCompletionCondition();
}