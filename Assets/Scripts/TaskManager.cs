using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{    
    public string[] TasksTextArray;
    public List<bool> CompletedTasks;
    private GameManager gameManager;
    private TriggerTaskEvents[] triggerTaskEvents;

    private void Awake()
    {        
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        triggerTaskEvents = FindObjectsOfType<TriggerTaskEvents>().ToArray();
        triggerTaskEvents.ToList().ForEach(t => t.TriggerTaskEvent += SetResult);        
    }
    private void OnDisable()
    {
        triggerTaskEvents.ToList().ForEach(t => t.TriggerTaskEvent -= SetResult);
    }

    public void SetResult(string taskName)
    {
        print("Засчитываю - " + taskName);
        for (int i = 1; i < TasksTextArray.Length; i++)
        {
            if (TasksTextArray[i] == taskName)
            {
                TasksTextArray[i] = "<color=lime>" + taskName + "</color>";
                CompletedTasks[i-1] = true;
            }
        }
        gameManager.SetTaskText(TasksTextArray);
    }
}
