using UnityEngine;

public delegate void OnEnd();
public delegate void AddMiddleMistake();
public delegate void CountScroreOfTasks();
public class Exercise : MonoBehaviour
{
    public ExercisesScriptableObject exercisesScriptable;
    public int MaxScore;
    public int CountPathLinesLeft;

    public event OnEnd OnEndEvent;
    public event AddMiddleMistake AddMiddleMistake;
    public event CountScroreOfTasks CountScroreOfTasks;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject PlayerWithTrailer;
    [SerializeField]
    private Transform StartPoint;

    void Start()
    {
        GameObject tractor = Instantiate(exercisesScriptable.IsParkWithTailer == true ? PlayerWithTrailer : Player, StartPoint.position, StartPoint.rotation);
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

    public void AddMiddleMistakeInvoke()
    {
        AddMiddleMistake?.Invoke();
    }

    public void EndExercise(bool withMiddleMistake)
    {
        if (withMiddleMistake) AddMiddleMistakeInvoke();

        CountScroreOfTasks?.Invoke();

        int currentScore = GameManager.CurrentScore;       
        exercisesScriptable.IsPassed = currentScore < 5;
        exercisesScriptable.Score = currentScore;
        OnEndEvent?.Invoke();
    }
}
