namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using UnityEditor;
    using VUDK.Editor.Constants;
    using static VUDK.Features.More.DialogueSystem.Editor.Constants.DSEditorPaths;

    public class DSSettingsWindow
    {
        private const string Label = "Dialogue System";

        private static bool s_useDefaultPath = true;
        private static string s_customDialoguesSavePath;

        [SettingsProvider]
        public static SettingsProvider DialogueSystemSettings()
        {
            var provider = new SettingsProvider($"{EditorConstants.VUDKPrefSettings}/{Label}", SettingsScope.User)
            {
                label = Label,
                guiHandler = (searchContext) =>
                {
                    EditorGUI.BeginChangeCheck();
                    s_useDefaultPath = EditorGUILayout.Toggle("Use Default Save Path", s_useDefaultPath);
                    s_customDialoguesSavePath = DialoguesSaveParentFolderPath;

                    if (s_useDefaultPath)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.TextField("Dialogues Save Path", DefaultDialoguesSaveParentFolderPath);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        s_customDialoguesSavePath = EditorGUILayout.TextField("Dialogues Save Path", s_customDialoguesSavePath);
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        if (s_useDefaultPath)
                            s_customDialoguesSavePath = DefaultDialoguesSaveParentFolderPath;

                        EditorPrefs.SetString(EditorConstants.VUDKPrefSettings, s_customDialoguesSavePath);
                        ChangeDialoguesSavePath(s_customDialoguesSavePath);
                    }
                },
                keywords = new string[] { "Dialogues", "Save", "Path" }
            };

            return provider;
        }
    }
}
