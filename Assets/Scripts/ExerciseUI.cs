using UnityEngine;
using UnityEngine.UI;

public class ExerciseUI : MonoBehaviour
{
    public ExercisesScriptableObject ExercisesScriptable;

    [SerializeField]
    private Text attempsText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text markText;

    public Button StartButton;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        attempsText.text = "Попытки: " + ExercisesScriptable.Attempts + "/2";
        scoreText.text = "Штрафные баллы - " + ExercisesScriptable.Score;
        markText.text = "Оценка - " + (ExercisesScriptable.IsPassed == true ? "сдал" : "не сдал");

        StartButton.interactable = ExercisesScriptable.Attempts < 2;
    }
}
