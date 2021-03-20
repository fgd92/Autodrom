using UnityEngine;

[CreateAssetMenu(fileName = "DataExercise", menuName = "ScriptableObjects/ExercisesScriptableObject", order = 1)]
public class ExercisesScriptableObject : ScriptableObject
{    
    public int Attempts;
    public int Score;
    public bool IsPassed;
    public bool PrematureTermination;
    public bool IsParkWithTailer;
    public bool IsEstacada;
    public bool IsPark;
    [Multiline()]
    public string Description;
}
