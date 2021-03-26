using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObject;
    [SerializeField]
    private GameObject leftMenuPlane;
    [SerializeField]
    private GameObject chooseMenuPlane;
    [SerializeField]
    private GameObject listExerciseObject;
    [SerializeField]
    private GameObject howToPlayObject;
    [SerializeField]
    private Transform exerciseContent;
    [SerializeField]
    private Text examResultText;

    private void Start()
    {
        EnableMenu();

        examResultText.text = CheckExercisesResults() == true ? "Оценка экзамена: \n <color=lime>сдал</color>" : "Оценка экзамена: \n <color=red>не сдал</color>";
    }

    public void EnableMenu()
    {
        menuObject.SetActive(true);
        leftMenuPlane.SetActive(true);
        listExerciseObject.SetActive(false);
        chooseMenuPlane.SetActive(false);
        howToPlayObject.SetActive(false);
    }

    public void EnableList()
    {
        menuObject.SetActive(false);
        leftMenuPlane.SetActive(false);
        listExerciseObject.SetActive(true);
        chooseMenuPlane.SetActive(true);
        howToPlayObject.SetActive(false);
    }
    public void EnableHowToPlayMenu()
    {
        menuObject.SetActive(false);
        leftMenuPlane.SetActive(false);
        listExerciseObject.SetActive(false);
        chooseMenuPlane.SetActive(true);
        howToPlayObject.SetActive(true);
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
        for (int i = 0; i < exerciseContent.childCount; i++)
        {
            ExerciseUI exerciseUI = exerciseContent.GetChild(i).GetComponent<ExerciseUI>();
            exerciseUI.ExercisesScriptable.Attempts = 0;
            exerciseUI.ExercisesScriptable.Score = 0;
            exerciseUI.ExercisesScriptable.IsPassed = false;
            exerciseUI.ExercisesScriptable.PrematureTermination = false;
            exerciseUI.UpdateUI();
        }
    }

    private bool CheckExercisesResults()
    {
        int mistakeCount = 0;

        for (int i = 0; i < exerciseContent.childCount; i++)
        {
            ExerciseUI exerciseUI = exerciseContent.GetChild(i).GetComponent<ExerciseUI>();
            if (!exerciseUI.ExercisesScriptable.IsPassed)
                mistakeCount += 1;
        }

        return mistakeCount < 2;
    }
}
