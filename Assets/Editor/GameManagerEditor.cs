using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    GameManager gameManager;
    private void OnEnable()
    {
        gameManager = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Добавить"))
        {
            gameManager.ExercisesListObjects.Add(null);
        }
        if (GUILayout.Button("Удалить"))
        {
            gameManager.ExercisesListObjects.Remove(gameManager.ExercisesListObjects[gameManager.ExercisesListObjects.Count - 1]);
        }

        EditorGUILayout.BeginVertical("box");
        for (int i = 0; i < gameManager.ExercisesListObjects.Count; i++)
        {
            GameObject gameObject = gameManager.ExercisesListObjects[i];
            gameObject = (GameObject)EditorGUILayout.ObjectField(string.Format("Упражнение {0}",i), gameObject, typeof(GameObject), true);

            EditorGUILayout.BeginHorizontal();
            if (gameManager.ExercisesListObjects[i] != null)
            {
                Exercise exercise = gameObject.GetComponent<Exercise>();
                exercise.MaxScore = EditorGUILayout.IntField("Макс. очков", exercise.MaxScore);                
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndVertical();
    }
}