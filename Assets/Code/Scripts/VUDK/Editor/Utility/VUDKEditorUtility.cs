namespace VUDK.Editor.Utility
{
    using System;
    using UnityEditor;

    public static class VUDKEditorUtility
    {
        public static void DrawFoldout(ref bool isShowed, string label, Action callback)
        {
            isShowed = EditorGUILayout.BeginFoldoutHeaderGroup(isShowed, label);
            if (isShowed) callback?.Invoke();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void DrawDisabledGroup(Action callback)
        {
            EditorGUI.BeginDisabledGroup(true);
            callback?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHorizontal(Action callback)
        {
            EditorGUILayout.BeginHorizontal();
            callback?.Invoke();
            EditorGUILayout.EndHorizontal();
        }

        public static void DrawVertical(Action callback)
        {
            EditorGUILayout.BeginVertical();
            callback?.Invoke();
            EditorGUILayout.EndVertical();
        }

        public static void DrawHeaderLabel(string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        }

        public static void DrawSpace(int space = 4)
        {
            EditorGUILayout.Space(space);
        }

        public static void DrawIndented(int indentLevel, Action callback)
        {
            EditorGUI.indentLevel+=indentLevel;
            callback?.Invoke();
            EditorGUI.indentLevel-=indentLevel;
        }
    }
}