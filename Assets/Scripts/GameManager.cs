using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Упражнения")]
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
    [Header("Меню паузы")]
    public GameObject PauseMenu;
    public Button PauseRestartButton;

    private Exercise exercise;

    public void Start()
    {
        LockCursor(true);

        DisplayEndmenu(false);

        GameObject ExerciseObject = ExercisesListObjects[CurrentExercise];
        ExerciseObject.SetActive(true);
        exercise = ExerciseObject.GetComponent<Exercise>();
        exercise.OnEndEvent += Exercise_OnEndEvent;
        exercise.exercisesScriptable.Attempts += 1;

        SetUI();

        FindActiveConus(ExerciseObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);            
            Time.timeScale = PauseMenu.activeSelf == true ? 0 : 1;
            LockCursor(!PauseMenu.activeSelf);
            PauseRestartButton.interactable = exercise.exercisesScriptable.Attempts < 2; 
        }
    }

    public void LoadSceneFromPauseMenu(int idScene)
    {             
        LoadScene(idScene);
    }

    private static void LockCursor(bool isLock)
    {
        Cursor.visible = !isLock;
        Cursor.lockState = isLock==true ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void DisplayEndmenu(bool enable)
    {
        GameUI.SetActive(!enable);
        EndMenu.SetActive(enable);
        PauseMenu.SetActive(false);
    }

    private void Exercise_OnEndEvent()
    {
        Time.timeScale = 0;
        LockCursor(false);
        DisplayEndmenu(true);
        MarkText.text = "Ваша оценка:  \n" + (exercise.exercisesScriptable.IsPassed == true ? "сдал" : "не сдал");
        RestartButton.interactable = exercise.exercisesScriptable.Attempts < 2;
        AttempsText.text = exercise.exercisesScriptable.Attempts < 2 ? "У вас есть еще одна попытка" : "Ваши попытки кончились";
        if (!exercise.exercisesScriptable.PrematureTermination)
            EndScoreText.text = "Вы набрали " + CurrentScore + " штрафных баллов";
        else
            EndScoreText.text = "Вы выехали за границу упражнения";
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
