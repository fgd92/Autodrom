using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    public List<GameObject> ExercisesListObjects;
    public static int CurrentExercise;    
    public static int CurrentScore;
    
    public Text MaxScoretext;
    public Text CurrentScoreText;

    private Exercise exercise;

    public void Start()
    {
        GameObject ExerciseObject = ExercisesListObjects[CurrentExercise];
        ExerciseObject.SetActive(true);
        exercise = ExerciseObject.GetComponent<Exercise>();
        exercise.OnEndEvent += Exercise_OnEndEvent;

        SetUI();

        FindActiveConus(ExerciseObject);
    }

    private void Exercise_OnEndEvent()
    {
        LoadMenu();
        CurrentScore = 0;
    }

    private void FindActiveConus(GameObject exerciseObject)
    {
        for (int i = 0; i < exerciseObject.transform.GetChild(0).childCount; i++)
        {
            exerciseObject.transform.GetChild(0).GetChild(i).GetComponent<Conus>().AddScoreEvent += GameManager_AddScoreEvent;            
        }
    }

    private void GameManager_AddScoreEvent()
    {
        CurrentScore += 1;
        SetUI();
    }

    private void SetUI()
    {
        MaxScoretext.text = "Максимальное количество штрафных баллов - " + exercise.MaxScore;
        CurrentScoreText.text = "Штрафные баллы: " + CurrentScore;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
