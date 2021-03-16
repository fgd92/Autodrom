using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataExercise", menuName = "ScriptableObjects/ExercisesScriptableObject", order = 1)]
public class ExercisesScriptableObject : ScriptableObject
{    
    public int Attempts;
    public int Score;
    public bool IsPassed;

}
