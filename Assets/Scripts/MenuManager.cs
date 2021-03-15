using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject ListExerciseObject;
    public ExercisesScriptableObject exercisesScriptable;    

    private void Start()
    {
        EnableMenu();
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
        exercisesScriptable.CurrentExercise = idExercise;
        SceneManager.LoadScene(2);
    }
}
