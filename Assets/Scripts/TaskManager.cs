using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    public string[] TasksTextArray;

    private GameManager gameManager;

    private void Awake()
    {
        Instance = this;
        gameManager = GetComponent<GameManager>();
    }
    public void SetResult(string taskName)
    {
        for (int i = 1; i < TasksTextArray.Length; i++)
        {
            if (TasksTextArray[i].Contains(taskName))
            {
                TasksTextArray[i] = "<color=lime> - " + taskName +"\n" + "</color>";
            }
        }
        gameManager.SetTaskText(TasksTextArray);
    }
}
