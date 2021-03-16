using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject ListExerciseObject;
    public Transform ExerciseContent;

    public Text ExamResultText;

    private void Start()
    {
        EnableMenu();

        ExamResultText.text = CheckExercisesResults() == true ? "Оценка экзамена: \n сдал" : "Оценка экзамена: \n не сдал";
    }

    public void EnableMenu()
    {
        MenuObject.SetActive(true);
        ListExerciseObject.SetActive(false);
    }

    public void EnableList()
    {
        MenuObject.SetActive(false);
        ListExerciseObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadGame(int idExercise)
    {        
        GameManager.CurrentExercise = idExercise;
        SceneManager.LoadScene(2);
    }

    public void DeleteData()
    {
        for (int i = 0; i < ExerciseContent.childCount; i++)
        {
            ExerciseUI exerciseUI = ExerciseContent.GetChild(i).GetComponent<ExerciseUI>();
            exerciseUI.exercisesScriptable.Attempts = 0;
            exerciseUI.exercisesScriptable.Score = 0;
            exerciseUI.exercisesScriptable.IsPassed = false;
            exerciseUI.UpdateUI();
        }
    }

    private bool CheckExercisesResults()
    {
        int mistakeCount = 0;

        for (int i = 0; i < ExerciseContent.childCount; i++)
        {
            ExerciseUI exerciseUI = ExerciseContent.GetChild(i).GetComponent<ExerciseUI>();
            if (!exerciseUI.exercisesScriptable.IsPassed)
                mistakeCount += 1;
        }

        return mistakeCount < 2;
    }
}
