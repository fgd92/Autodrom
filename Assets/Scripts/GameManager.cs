using System.Collections;
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
    public Text TaskListText;
    public GameObject GameUI;
    public Image AttentionSignImage;
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
    private TaskManager taskManager;
    private void Awake()
    {
        taskManager = GetComponent<TaskManager>();
    }
    public void Start()
    {
        LockCursor(true);

        DisplayEndmenu(false);

        GameObject ExerciseObject = ExercisesListObjects[CurrentExercise];
        ExerciseObject.SetActive(true);
        exercise = ExerciseObject.GetComponent<Exercise>();
        exercise.OnEndEvent += Exercise_OnEndEvent;
        exercise.AddMiddleMistake += Exercise_AddMiddleMistake;
        exercise.exercisesScriptable.Attempts += 1;

        SetUI();

        TaskListText.text = exercise.exercisesScriptable.Description;
        taskManager.TasksTextArray = TaskListText.text.Split('-');

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

        Unsubscribe();
    }

    private void Unsubscribe()
    {
        for (int i = 0; i < exercise.transform.GetChild(0).childCount-1; i++)
        {
            exercise.transform.GetChild(0).GetChild(i).GetComponent<Conus>().AddGrossMistake -= GameManager_AddScoreEvent;
        }
        exercise.OnEndEvent -= Exercise_OnEndEvent;
        exercise.AddMiddleMistake -= Exercise_AddMiddleMistake;
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
        for (int i = 0; i < exerciseObject.transform.GetChild(0).childCount-1; i++)
        {
            Transform gameObject = exerciseObject.transform.GetChild(0).GetChild(i);
            gameObject.GetComponent<Conus>().AddGrossMistake += GameManager_AddScoreEvent;

        }
    }
    private void Exercise_AddMiddleMistake()
    {
        CurrentScore += 3;
        SetUI();
        StopCoroutine(nameof(AnimationAttentionSign));
        StartCoroutine(nameof(AnimationAttentionSign), 2f);
    }

    private void GameManager_AddScoreEvent()
    {
        CurrentScore += 5;
        SetUI();
        StopCoroutine(nameof(AnimationAttentionSign));
        StartCoroutine(nameof(AnimationAttentionSign), 2f);
    }

    private void SetUI()
    {
        MaxScoretext.text = "Максимальное количество штрафных баллов - " + exercise.MaxScore;
        CurrentScoreText.text = "Штрафные баллы: " + CurrentScore;
    }

    public void SetTaskText(string[] TasksTextArray)
    {
        TaskListText.text = "";
        for (int i = 0; i < TasksTextArray.Length; i++)
        {
            if (i == 0)
                TaskListText.text += TasksTextArray[i];
            else if (!TasksTextArray[i].Contains("-"))
                TaskListText.text += "-" + TasksTextArray[i];
        }
    }

    private IEnumerator AnimationAttentionSign(float speed)
    {
        AttentionSignImage.gameObject.SetActive(true);
        AttentionSignImage.color = new Color(AttentionSignImage.color.r, AttentionSignImage.color.g, AttentionSignImage.color.b, 1);

        while (AttentionSignImage.color.a > 0)
        {
            AttentionSignImage.color = new Color(AttentionSignImage.color.r, AttentionSignImage.color.g, AttentionSignImage.color.b, AttentionSignImage.color.a - (Time.deltaTime * speed));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (AttentionSignImage.color.a < 1)
        {
            AttentionSignImage.color = new Color(AttentionSignImage.color.r, AttentionSignImage.color.g, AttentionSignImage.color.b, AttentionSignImage.color.a + (Time.deltaTime * speed));
            yield return null;
        }

        while (AttentionSignImage.color.a > 0)
        {
            AttentionSignImage.color = new Color(AttentionSignImage.color.r, AttentionSignImage.color.g, AttentionSignImage.color.b, AttentionSignImage.color.a - (Time.deltaTime * speed));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        AttentionSignImage.gameObject.SetActive(false);
    }
   

    public void LoadScene(int idScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(idScene);
    }
}
