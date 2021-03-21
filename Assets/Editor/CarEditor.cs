using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

[CustomEditor(typeof(CarBase))]
public class CarEditor : Editor
{
    private CarBase car;


    private void OnEnable()
    {
        car = (CarBase)target;
        RefreshEffectListEditor(car.ComponentProfile);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
    void RefreshEffectListEditor(ComponentProfile asset)
    {

    }
}
