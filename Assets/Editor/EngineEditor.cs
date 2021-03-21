using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(Engine))]
public class EngineEditor : CarComponentEditor
{
    Engine engine;

    private void OnEnable()
    {
        engine = (Engine)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        FieldInfo MotorForce = typeof(Engine).GetField("motorForce", BindingFlags.Instance | BindingFlags.NonPublic);
        float motorForce = (float)MotorForce.GetValue(engine);
        MotorForce.SetValue(engine, EditorGUILayout.FloatField("Motor force", motorForce));
        EditorGUILayout.EndVertical();
    }
}