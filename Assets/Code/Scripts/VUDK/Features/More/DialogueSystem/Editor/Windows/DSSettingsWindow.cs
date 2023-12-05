namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using UnityEditor;
    using VUDK.Editor.Constants;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;

    public class DSSettingsWindow
    {
        private static string s_label = "Dialogue System";
        private static bool useDefaultPath = true; // Aggiunto: Per tenere traccia di utilizzare il percorso predefinito o meno
        private static string customDialoguesSavePath;

        [SettingsProvider]
        public static SettingsProvider MySettings()
        {
            var provider = new SettingsProvider($"{EditorConstants.VUDKPrefSettings}/{s_label}", SettingsScope.User)
            {
                label = s_label,
                guiHandler = (searchContext) =>
                {
                    EditorGUI.BeginChangeCheck();
                    useDefaultPath = EditorGUILayout.Toggle("Use Default Save Path", useDefaultPath);
                    customDialoguesSavePath = DSIOUtility.DialoguesSaveParentFolderPath;

                    if (useDefaultPath)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.TextField("Dialogues Save Path", DSIOUtility.DefaultDialoguesSaveParentFolderPath);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        customDialoguesSavePath = EditorGUILayout.TextField("Dialogues Save Path", customDialoguesSavePath);
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        if (useDefaultPath)
                            customDialoguesSavePath = DSIOUtility.DefaultDialoguesSaveParentFolderPath;

                        EditorPrefs.SetString(EditorConstants.VUDKPrefSettings, customDialoguesSavePath);
                        DSIOUtility.ChangeDialoguesSavePath(customDialoguesSavePath);
                    }
                },
                keywords = new string[] { "Dialogues", "Save", "Path" }
            };

            return provider;
        }
    }
}
