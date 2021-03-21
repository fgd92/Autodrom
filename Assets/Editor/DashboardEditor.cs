using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dashboard))]
public class DashboardEditor : CarComponentEditor
{
    Dashboard dashboard;

    void OnEnable()
    {
        dashboard = (Dashboard)target;
    }

    public override void OnInspectorGUI()
    {
        if (dashboard.Measures == null)
        {
            dashboard.Measures = new Measures();
        }

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < dashboard.Measures.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            dashboard.Measures[i] = (Measure)EditorGUILayout.ObjectField("Measure device", dashboard.Measures[i], typeof(Measure), true);

            if (dashboard.Measures[i] != null)
            {
                dashboard.Measures[i].TypeMeasure = (TypeMeasure)EditorGUILayout.EnumPopup("Type measure", dashboard.Measures[i].TypeMeasure);
                ViewSettingAngle(dashboard.Measures[i]);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add", GUILayout.Width(135), GUILayout.Height(25)))
        {
            dashboard.Measures.Add(null);
        }
        if (GUILayout.Button("Remove", GUILayout.Width(135), GUILayout.Height(25)))
        {
            dashboard.Measures.Remove(dashboard.Measures.Last());
        }
        EditorGUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(dashboard);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            for (int i = 0; i < dashboard.Measures.Count; i++)
            {
                if(dashboard.Measures.Items[i] != null)
                    EditorUtility.SetDirty(dashboard.Measures.Items[i]);
            }
        }

    }
    private void ViewSettingAngle(Measure measure)
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal("box");

        EditorGUILayout.BeginVertical();
        EditorGUILayout.PrefixLabel("Limit arrow");
        EditorGUILayout.BeginHorizontal();

        FieldInfo MinAngle = typeof(Measure).GetField("MinAngle", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo MaxAngle = typeof(Measure).GetField("MaxAngle", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo MaxValue = typeof(Measure).GetField("MaxValue", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo CurrentValue = typeof(Measure).GetField("currentValue", BindingFlags.Instance | BindingFlags.NonPublic);
        float minAngle = (float)MinAngle.GetValue(measure);
        float maxAngle = (float)MaxAngle.GetValue(measure);
        float maxValue = (float)MaxValue.GetValue(measure);
        float currentValue = (float)CurrentValue.GetValue(measure);
        GUILayout.Label("-180", GUILayout.Width(40));
        EditorGUILayout.MinMaxSlider(ref minAngle, ref maxAngle, -180, 180f);
        GUILayout.Label("180", GUILayout.Width(40));

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        minAngle = EditorGUILayout.FloatField("Min angle", minAngle);
        maxAngle = EditorGUILayout.FloatField("Max angle", maxAngle);
        EditorGUILayout.EndVertical();

        EditorGUILayout.FloatField("Max value", maxValue);
        MinAngle.SetValue(measure, minAngle);
        MaxAngle.SetValue(measure, maxAngle);
        MaxValue.SetValue(measure, maxValue);
        EditorGUILayout.Slider(currentValue, 0, maxValue);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }
}
