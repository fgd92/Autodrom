using System;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    public static bool DrawHeaderToggle(string title, SerializedProperty group, SerializedProperty activeField, Action<Vector2> contextAction = null, Func<bool> hasMoreOptions = null, Action toggleMoreOptions = null)
               => DrawHeaderToggle(EditorGUIUtility.TrTextContent(title), group, activeField, contextAction, hasMoreOptions, toggleMoreOptions);
    public static void DrawSplitter(bool isBoxed = false)
    {
        var rect = GUILayoutUtility.GetRect(1f, 1f);

        rect.xMin = 0f;
        rect.width += 4f;

        if (isBoxed)
        {
            rect.xMin = EditorGUIUtility.singleLineHeight;
            rect.width -= 1;
        }

        if (Event.current.type != EventType.Repaint)
            return;

        EditorGUI.DrawRect(rect, !EditorGUIUtility.isProSkin
            ? new Color(0.6f, 0.6f, 0.6f, 1.333f)
            : new Color(0.12f, 0.12f, 0.12f, 1.333f));
    }
    public static bool DrawHeaderToggle(GUIContent title, SerializedProperty group, SerializedProperty activeField, Action<Vector2> contextAction = null, Func<bool> hasMoreOptions = null, Action toggleMoreOptions = null)
    {
        var backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

        var labelRect = backgroundRect;
        labelRect.xMin += 32f;
        labelRect.xMax -= 20f + 16 + 5;

        var foldoutRect = backgroundRect;
        foldoutRect.y += 1f;
        foldoutRect.width = 13f;
        foldoutRect.height = 13f;

        var toggleRect = backgroundRect;
        toggleRect.x += 16f;
        toggleRect.y += 2f;
        toggleRect.width = 13f;
        toggleRect.height = 13f;

        // More options 1/2
        var moreOptionsRect = new Rect();
        if (hasMoreOptions != null)
        {
            moreOptionsRect = backgroundRect;
            moreOptionsRect.x += moreOptionsRect.width - 16 - 1 - 16 - 5;
            moreOptionsRect.height = 15;
            moreOptionsRect.width = 16;
        }

        // Background rect should be full-width
        backgroundRect.xMin = 0f;
        backgroundRect.width += 4f;

        // Background
        float backgroundTint = EditorGUIUtility.isProSkin ? 0.1f : 1f;
        EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

        // Title
        using (new EditorGUI.DisabledScope(!activeField.boolValue))
            EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

        // Foldout
        group.serializedObject.Update();
        group.isExpanded = GUI.Toggle(foldoutRect, group.isExpanded, GUIContent.none, EditorStyles.foldout);
        group.serializedObject.ApplyModifiedProperties();

        // Active checkbox
        activeField.serializedObject.Update();
        activeField.boolValue = GUI.Toggle(toggleRect, activeField.boolValue, GUIContent.none);
        activeField.serializedObject.ApplyModifiedProperties();

        // More options 2/2
        if (hasMoreOptions != null)
        {
            bool moreOptions = hasMoreOptions();
            if (moreOptions)
                toggleMoreOptions?.Invoke();
        }

        // Context menu
        float width = 0;
        float height = 0;
        var menuRect = new Rect(labelRect.xMax + 3f + 16 + 5, labelRect.y + 1f, width, height);

        if (contextAction != null)
            GUI.DrawTexture(menuRect, null);

        // Handle events
        var e = Event.current;

        if (e.type == EventType.MouseDown)
        {
            if (contextAction != null && menuRect.Contains(e.mousePosition))
            {
                contextAction(new Vector2(menuRect.x, menuRect.yMax));
                e.Use();
            }
            else if (labelRect.Contains(e.mousePosition))
            {
                if (e.button == 0)
                    group.isExpanded = !group.isExpanded;
                else if (contextAction != null)
                    contextAction(e.mousePosition);

                e.Use();
            }
        }

        return group.isExpanded;
    }
}