using UnityEngine;

[CreateAssetMenu(fileName = "DataExercise", menuName = "ScriptableObjects/ExercisesScriptableObject", order = 1)]
public class ExercisesScriptableObject : ScriptableObject
{    
    public int Attempts;
    public int Score;
    public string timer;
    public bool IsPassed;
    public bool PrematureTermination;
    public bool IsParkWithTailer;
    public bool IsEstacada;
    public bool IsPark;
    public bool isFreeRide;
    [Multiline(10)]
    public string Description;    
}
