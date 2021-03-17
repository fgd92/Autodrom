﻿using UnityEngine;

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
        exercisesScriptable.PrematureTermination = false;
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {            
            int currentScore = GameManager.CurrentScore;

            exercisesScriptable.IsPassed = currentScore < 5;            
            exercisesScriptable.Score = currentScore;
            
            OnEndEvent?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].thisCollider.CompareTag("Border"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                exercisesScriptable.IsPassed = false;                
                exercisesScriptable.Score = 5;
                exercisesScriptable.PrematureTermination = true;

                OnEndEvent?.Invoke();
            }
        }
    }
}
