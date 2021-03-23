using UnityEngine;

public delegate void OnEnd();
public delegate void AddMistake(int scoreCount);
public delegate void CountScroreOfTasks();
public class Exercise : MonoBehaviour
{
    public ExercisesScriptableObject exercisesScriptable;
    public int MaxScore;
    public int CountPathLinesLeft;

    public event OnEnd OnEndEvent;
    public event AddMistake AddMistake;
    public event CountScroreOfTasks CountScroreOfTasks;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerWithTrailer;
    [SerializeField]
    private Transform startPoint;     

    void Start()
    {
        GameObject tractor = Instantiate(exercisesScriptable.IsParkWithTailer == true ? playerWithTrailer : player, startPoint.position, startPoint.rotation);
        Destroy(tractor.GetComponent<HingeJoint>());
        exercisesScriptable.PrematureTermination = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Trailer"))
        {
            if (collision.contacts[0].thisCollider.CompareTag("Border"))
            {            
                exercisesScriptable.IsPassed = false;                
                exercisesScriptable.Score = 5;
                exercisesScriptable.PrematureTermination = true;

                OnEndEvent?.Invoke();
            }
        }
    }

    public void AddMistakeInvoke(int scoreCount)
    {
        AddMistake?.Invoke(scoreCount);
    }

    public void EndExercise(bool withMiddleMistake)
    {
        if (withMiddleMistake) AddMistakeInvoke(3);

        CountScroreOfTasks?.Invoke();

        int currentScore = GameManager.CurrentScore;       
        exercisesScriptable.IsPassed = currentScore < 5;
        exercisesScriptable.Score = currentScore;
        OnEndEvent?.Invoke();
    }
}
