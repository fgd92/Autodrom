using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField]
    private AudioMixerSnapshot normal;
    [SerializeField]
    private AudioMixerSnapshot inMenu;
    [SerializeField]
    private AudioMixerGroup mixerGroup;
    [SerializeField]
    private Color enableColor;
    [SerializeField]
    private Color disableColor;
    [SerializeField]
    private Image soundImage;
    private float currentVolume;

    private void Start()
    {
        EnableMenu();
        SetSoundImageColor(soundImage);
        examResultText.text = CheckExercisesResults() == true ? "Оценка экзамена: \n <color=lime>сдал</color>" : "Оценка экзамена: \n <color=red>не сдал</color>";
    }

    public void EnableMenu()
    {
        menuObject.SetActive(true);
        leftMenuPlane.SetActive(true);
        listExerciseObject.SetActive(false);
        chooseMenuPlane.SetActive(false);
        howToPlayObject.SetActive(false);
        normal.TransitionTo(1f);
    }

    public void EnableList()
    {
        menuObject.SetActive(false);
        leftMenuPlane.SetActive(false);
        listExerciseObject.SetActive(true);
        chooseMenuPlane.SetActive(true);
        howToPlayObject.SetActive(false);
        inMenu.TransitionTo(1f);
    }
    public void EnableHowToPlayMenu()
    {
        menuObject.SetActive(false);
        leftMenuPlane.SetActive(false);
        listExerciseObject.SetActive(false);
        chooseMenuPlane.SetActive(true);
        howToPlayObject.SetActive(true);
        inMenu.TransitionTo(1f);
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
            exerciseUI.ExercisesScriptable.timer = "00:00";
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
    public void ToggleSounds(Image image)
    {
        mixerGroup.audioMixer.GetFloat("SoundsVolume", out currentVolume);
        mixerGroup.audioMixer.SetFloat("SoundsVolume",  currentVolume == 0 ? -80 : 0);
        SetSoundImageColor(image);
    }
    private void SetSoundImageColor(Image image)
    {
        mixerGroup.audioMixer.GetFloat("SoundsVolume", out currentVolume);
        image.color = currentVolume == 0 ? enableColor : disableColor;
    }
}
