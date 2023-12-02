namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using UnityEditor;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using UnityEditor.UIElements;
    using VUDK.Extensions;

    public class DSEditorWindow : EditorWindow
    {
        private readonly string _defaultFileName = "New Dialogue Graph";

        private TextField _fileNameTextField;
        private Button _saveButton;

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

        public void EnableSaving()
        {
            _saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            _saveButton.SetEnabled(false);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            TextField fileNameTextField = DSElementUtility.CreateTextField(_defaultFileName, "File Name:", callback =>
            {
                _fileNameTextField.value = callback.newValue.RemoveSpecialAndWhitespaces();
            });

            _saveButton = DSElementUtility.CreateButton("Save");
            toolbar.Add(fileNameTextField);
            toolbar.Add(_saveButton);
            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");

            rootVisualElement.Add(toolbar);
        }

        private void AddGraphView()
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