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
    [SerializeField]
    private Text maxScoreText;
    [SerializeField]
    private Text currentScoreText;
    [SerializeField]
    private Text taskListText;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private Image attentionSignImage;

    [Header("Меню проигрыша")]
    [SerializeField]
    private Text attempsText;
    [SerializeField]
    private Text markText;
    [SerializeField]
    private Text endScoreText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private GameObject endMenu;

    [Header("Меню паузы")]
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Button pauseRestartButton;

    private bool isEnd;
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
        exercise.CountScroreOfTasks += Exercise_CountScroreOfTasks;
        exercise.exercisesScriptable.Attempts += 1;

        SetUI();
        SetTaskManagerArray();

        FindActiveConus(ExerciseObject);
    }

    private void Exercise_CountScroreOfTasks()
    {
        for (int i = 0; i < taskManager.CompletedTasks.Count; i++)
        {
            if (!taskManager.CompletedTasks[i])
                CurrentScore += 5;
        }
    }

    private void SetTaskManagerArray()
    {
        taskListText.text = exercise.exercisesScriptable.Description;
        taskManager.TasksTextArray = taskListText.text.Split('\n');

        taskManager.CompletedTasks = new List<bool>();
        for (int i = 1; i < taskManager.TasksTextArray.Length-1; i++)
        {
            taskManager.CompletedTasks.Add(false);
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnd)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);            
            Time.timeScale = pauseMenu.activeSelf == true ? 0 : 1;
            LockCursor(!pauseMenu.activeSelf);
            pauseRestartButton.interactable = exercise.exercisesScriptable.Attempts < 2; 
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            taskListText.gameObject.SetActive(!taskListText.gameObject.activeSelf);
        }
    }

    public void LoadSceneFromPauseMenu(int idScene)
    {
        Unsubscribe();

        LoadScene(idScene);
    }

    private void Unsubscribe()        
    {
        Transform conuses = exercise.transform.GetChild(0);
        for (int i = 0; i < conuses.childCount-1; i++)
        {
            conuses.GetChild(i).GetComponent<Conus>().AddGrossMistake -= GameManager_AddScoreEvent;
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
        gameUI.SetActive(!enable);
        endMenu.SetActive(enable);
        pauseMenu.SetActive(false);
    }

    private void Exercise_OnEndEvent()
    {
        Time.timeScale = 0;
        LockCursor(false);
        DisplayEndmenu(true);
        markText.text = "Ваша оценка:  \n" + (exercise.exercisesScriptable.IsPassed == true ? "сдал" : "не сдал");
        restartButton.interactable = exercise.exercisesScriptable.Attempts < 2;
        attempsText.text = exercise.exercisesScriptable.Attempts < 2 ? "У вас есть еще одна попытка" : "Ваши попытки кончились";
        if (!exercise.exercisesScriptable.PrematureTermination)
            endScoreText.text = "Вы набрали " + CurrentScore + " штрафных баллов";
        else
            endScoreText.text = "Вы выехали за границу упражнения";
        CurrentScore = 0;
        isEnd = true;
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
        maxScoreText.text = "Максимальное количество штрафных баллов - " + exercise.MaxScore;
        currentScoreText.text = "Штрафные баллы: " + CurrentScore;
    }

    public void SetTaskText(string[] TasksTextArray)
    {
        taskListText.text = "";
        for (int i = 0; i < TasksTextArray.Length; i++)
        {
            if (i == 0)
                taskListText.text += TasksTextArray[i] + "\n";            
            else if (i == TasksTextArray.Length-1)
                taskListText.text += TasksTextArray[i];
            else
                taskListText.text += TasksTextArray[i] + "\n";
        }
    }

    private IEnumerator AnimationAttentionSign(float speed)
    {
        attentionSignImage.gameObject.SetActive(true);
        attentionSignImage.color = new Color(attentionSignImage.color.r, attentionSignImage.color.g, attentionSignImage.color.b, 1);

        while (attentionSignImage.color.a > 0)
        {
            attentionSignImage.color = new Color(attentionSignImage.color.r, attentionSignImage.color.g, attentionSignImage.color.b, attentionSignImage.color.a - (Time.deltaTime * speed));
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (attentionSignImage.color.a < 1)
        {
            attentionSignImage.color = new Color(attentionSignImage.color.r, attentionSignImage.color.g, attentionSignImage.color.b, attentionSignImage.color.a + (Time.deltaTime * speed));
            yield return null;
        }

        while (attentionSignImage.color.a > 0)
        {
            attentionSignImage.color = new Color(attentionSignImage.color.r, attentionSignImage.color.g, attentionSignImage.color.b, attentionSignImage.color.a - (Time.deltaTime * speed));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        attentionSignImage.gameObject.SetActive(false);
    }
   

    public void LoadScene(int idScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(idScene);
    }
}
