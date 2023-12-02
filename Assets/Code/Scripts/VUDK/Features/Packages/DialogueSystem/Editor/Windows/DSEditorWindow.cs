namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using System;
    using UnityEditor;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;

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
            DSGraphView graphView = new DSGraphView(this);

            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }
    }
}