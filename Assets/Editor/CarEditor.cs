using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(CarBase))]
public class CarEditor : Editor
{
    private CarBase car;

    private void OnEnable()
    {
        car = (CarBase)target;

        if (car.ComponentProfile == null)
        {
            car.ComponentProfile = new ComponentProfile();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add", GUILayout.Width(120), GUILayout.Height(40)))
        {
            car.ComponentProfile.Add(new CarComponent());
        }
        if (GUILayout.Button("Remove" ,GUILayout.Width(120), GUILayout.Height(40)))
        {
            car.ComponentProfile.RemoveLast();
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < car.ComponentProfile.Count(); i++)
        {
            if (car.ComponentProfile.Get(i) != null)
            {
                var temp = car.ComponentProfile[i];
                var carComponent = (CarComponent)EditorGUILayout.ObjectField(
                    temp.displayName,
                    temp, temp.GetType(), true);

                car.ComponentProfile[i] = carComponent;
            }
        }

        EditorGUILayout.EndVertical();
    }
}