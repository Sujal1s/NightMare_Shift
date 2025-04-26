using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<BaseTask> tasks = new List<BaseTask>();
    
    [Header("Task Dependencies")]
    [SerializeField] private List<TaskDependency> taskDependencies = new List<TaskDependency>();

    private void Start()
    {
        InitializeTasks();
    }

    private void Update()
    {
        UpdateTasks();
    }

    private void InitializeTasks()
    {
        foreach (var task in tasks)
        {
            // Apply initial settings based on dependencies
            bool shouldBeActive = true;
            foreach (var dependency in taskDependencies)
            {
                if (dependency.dependentTask == task && !dependency.activeAtStart)
                {
                    shouldBeActive = false;
                    break;
                }
            }
            task.SetActive(shouldBeActive);
        }
    }

    private void UpdateTasks()
    {
        foreach (var task in tasks)
        {
            if (task.IsActive() && !task.IsCompleted() && task.CheckCompletionCondition())
            {
                task.CompleteTask();
                HandleDependencies(task);
            }
        }
    }

    private void HandleDependencies(BaseTask completedTask)
    {
        foreach (var dependency in taskDependencies)
        {
            if (dependency.prerequisiteTask == completedTask)
            {
                dependency.dependentTask.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class TaskDependency
{
    public BaseTask prerequisiteTask;
    public BaseTask dependentTask;
    public bool activeAtStart = false;
}