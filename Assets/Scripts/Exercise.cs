using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnEnd();
public class Exercise : MonoBehaviour
{
    public ExercisesScriptableObject exercisesScriptable;
    public GameObject Player;
    public Transform StartPoint;
    public int MaxScore;
    public event OnEnd OnEndEvent;

    void Start()
    {
        Instantiate(Player, StartPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentScore = GameManager.CurrentScore;

            exercisesScriptable.IsPassed = currentScore < 5;
            exercisesScriptable.Attempts += 1;
            exercisesScriptable.Score = currentScore;
            
            OnEndEvent?.Invoke();
        }
    }
}
