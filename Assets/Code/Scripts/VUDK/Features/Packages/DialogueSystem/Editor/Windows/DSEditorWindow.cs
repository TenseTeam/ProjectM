namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using System;
    using UnityEditor;
    using UnityEngine.UIElements;

    public class DSEditorWindow : EditorWindow
    {
        [MenuItem("VUDK/Dialogue Graph")]
        public static void Open()
        {
            DSEditorWindow window = GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            ConstructGraphView();
            AddStyles();
        }

        private void ConstructGraphView()
        {
            DSGraphView graphView = new DSGraphView();

            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet) EditorGUIUtility.Load("DialogueSystem/DSVariables.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}