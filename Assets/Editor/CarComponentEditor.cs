using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CarComponent), true)]
public class CarComponentEditor : Editor
{
    public SerializedObject serializedTarget;
    public CarComponent carComponent { get; private set; }
    public virtual string GetDisplayTitle()
    {
        return carComponent.displayName == "" ? ObjectNames.NicifyVariableName(carComponent.GetType().Name) : carComponent.displayName;
    }
    public virtual void OnEnable()
    {
        carComponent = (CarComponent)target;
        serializedTarget = new SerializedObject(carComponent);
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        for (int i = 0; i < carComponent.parameters.Count; i++)
        {
        }

        EditorGUILayout.EndVertical();
    }
}
