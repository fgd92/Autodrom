using UnityEngine;
using UnityEngine.UI;

public class ExerciseUI : MonoBehaviour
{
    public ExercisesScriptableObject exercisesScriptable;

    public Text AttempsText;
    public Text ScoreText;
    public Text MarkText;

    public Button StartButton;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        AttempsText.text = "Попытки: " + exercisesScriptable.Attempts + "/2";
        ScoreText.text = "Штрафные баллы - " + exercisesScriptable.Score;
        MarkText.text = "Оценка - " + (exercisesScriptable.IsPassed == true ? "сдал" : "не сдал");

        StartButton.interactable = exercisesScriptable.Attempts < 2;
    }
}
