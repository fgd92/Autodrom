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
    [Header("Игровое UI")]
    public Text MaxScoretext;
    public Text CurrentScoreText;
    public GameObject GameUI;
    [Header("Меню проигрыша")]
    public Text AttempsText;
    public Text MarkText;
    public Text EndScoreText;
    public Button RestartButton;
    public GameObject EndMenu;

    private Exercise exercise;

    public void Start()
    {
        DisplayEndmenu(false);

        GameObject ExerciseObject = ExercisesListObjects[CurrentExercise];
        ExerciseObject.SetActive(true);
        exercise = ExerciseObject.GetComponent<Exercise>();
        exercise.OnEndEvent += Exercise_OnEndEvent;

        SetUI();

        FindActiveConus(ExerciseObject);
    }

    private void DisplayEndmenu(bool enable)
    {
        GameUI.SetActive(!enable);
        EndMenu.SetActive(enable);
    }

    private void Exercise_OnEndEvent()
    {
        Time.timeScale = 0;
        DisplayEndmenu(true);
        MarkText.text = "Ваша оценка:  \n" + (exercise.exercisesScriptable.IsPassed == true ? "сдал" : "не сдал");
        RestartButton.interactable = exercise.exercisesScriptable.Attempts < 2;
        EndScoreText.text = "Вы набрали " + CurrentScore + " штрафных баллов";
        AttempsText.text = exercise.exercisesScriptable.Attempts < 2 ? "У вас есть еще одна попытка" : "Ваши попытки кончились";
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

    public void LoadScene(int idScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(idScene);
    }
}
