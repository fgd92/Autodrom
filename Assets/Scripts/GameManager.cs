using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ExercisesScriptableObject exercisesScriptable;
    public List<GameObject> ExercisesListObjects;
    public int CurrentScore;

    private Exercise exercise;

    public void Start()
    {
        GameObject ExerciseObject = ExercisesListObjects[exercisesScriptable.CurrentExercise];
        ExerciseObject.SetActive(true);
        exercise = ExerciseObject.GetComponent<Exercise>();
        exercise.OnEndEvent += Exercise_OnEndEvent;
    }

    private void Exercise_OnEndEvent()
    {
        print("Упражнение окончено");
    }
}
