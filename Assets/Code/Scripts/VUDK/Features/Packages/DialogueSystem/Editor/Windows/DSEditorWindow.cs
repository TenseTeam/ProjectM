namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;
    using VUDK.Extensions;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;

    public class DSEditorWindow : EditorWindow
    {
        private readonly string _defaultFileName = "New Dialogue Graph";
        private static TextField s_fileNameTextField;

        private DSGraphView _graphView;
        private Button _saveButton;
        private Button _loadButton;
        private Button _miniMapButton;

        [MenuItem("VUDK/Dialogue Graph")]
        public static void Open()
        {
            DSEditorWindow window = GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        public static void UpdateFileName(string newFileName)
        {
            s_fileNameTextField.value = newFileName;
        }

        public void EnableSaving()
        {
            _saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            _saveButton.SetEnabled(false);
        }

        private void AddGraphView()
        {
            _graphView = new DSGraphView(this);

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            s_fileNameTextField = DSElementUtility.CreateTextField(_defaultFileName, "File Name:", callback =>
            {
                s_fileNameTextField.value = callback.newValue.RemoveSpecialAndWhitespaces();
            });

            _saveButton = DSElementUtility.CreateButton("Save", () => Save());
            _loadButton = DSElementUtility.CreateButton("Load", () => Load());
            _miniMapButton = DSElementUtility.CreateButton("Mini Map", () => ToggleMiniMap());
            Button clearButton = DSElementUtility.CreateButton("Clear", () => Clear());
            Button resetButton = DSElementUtility.CreateButton("Reset", () => ResetGraph());

            toolbar.Add(s_fileNameTextField);
            toolbar.Add(_saveButton);
            toolbar.Add(_loadButton);
            toolbar.Add(clearButton);
            toolbar.Add(resetButton);
            toolbar.Add(_miniMapButton);
            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");

            rootVisualElement.Add(toolbar);
        }

        private void Clear()
        {
            _graphView.ClearGraph();
        }

        private void ResetGraph()
        {
            Clear();
            UpdateFileName(_defaultFileName);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(s_fileNameTextField.value))
            {
                EditorUtility.DisplayDialog(
                    "Invalid file name",
                    "Please enter a valid file name.",
                    "OK"
                    );

                return;
            }

            DSIOUtility.Init(_graphView, s_fileNameTextField.value);
            DSIOUtility.Save();
        }

        private void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Load Dialogue Graph", "Assets/Editor/DialogueSystem/Graphs", "asset");

            if(string.IsNullOrEmpty(filePath))
                return;

            Clear();
            DSIOUtility.Init(_graphView, Path.GetFileNameWithoutExtension(filePath));
            DSIOUtility.Load();
        }

        private void ToggleMiniMap()
        {
            _graphView.ToggleMiniMap();
            _miniMapButton.ToggleInClassList("ds-toolbar__button__selected");
        }
    }
}