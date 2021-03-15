﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataExercise", menuName = "ScriptableObjects/ExercisesScriptableObject", order = 1)]
public class ExercisesScriptableObject : ScriptableObject
{
    public List<string> ExerciseList;
    public int CurrentExercise;

    public void GetDataInListById(int id)
    {

    }
}
