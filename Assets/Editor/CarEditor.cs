using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CarBase))]
public class CarEditor : Editor
{
    private CarBase car;

    private void OnEnable()
    {
        car = (CarBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
