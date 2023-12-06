namespace VUDK.Features.More.DialogueSystem.Editor.Constants
{
    public static class DSEditorPaths
    {
        #region FIXED PATHS

        public const string DialoguesEditorFolderPath = "Assets/Code/Scripts/VUDK/Features/More/DialogueSystem/Editor";
        public const string DefaultDialoguesSaveParentFolderPath = "Assets/Code/Scripts/VUDK/Features/More/DialogueSystem";

        #endregion FIXED PATHS

        #region FOLDER NAMES

        public const string DialogueAssetMainFolderName = "DialougeAssets";

        #endregion FOLDER NAMES

        #region PREFERENCES PATHS

        public const string DSPreferencesSettings = "Preferences/VUDK";

        #endregion PREFERENCES PATHS

        #region EDITOR PROPERTY PATHS

        public static string DialoguesGraphsAssetPath => $"{DialoguesEditorFolderPath}/Graphs";
        public static string DialoguesEditorResourcesPath => $"{DialoguesEditorFolderPath}/Resources";
        public static string DialogueEditorIconsPath => $"{DialoguesEditorResourcesPath}/EditorIcons";
        public static string StyleSheetsPath => $"{DialoguesEditorResourcesPath}/Styles";

        #endregion EDITOR PROPERTY PATHS

        #region ASSET PROPERTY PATHS

        public static string DialoguesAssetPath => $"{DialoguesSaveParentFolderPath}/{DialogueAssetMainFolderName}";
        public static string DialoguesActorsAssetPath => $"{DialoguesAssetPath}/Actors";

        #endregion ASSET PROPERTY PATHS

        public static string DialoguesSaveParentFolderPath = DefaultDialoguesSaveParentFolderPath;

        public static void ChangeDialoguesSavePath(string path)
        {
            DialoguesSaveParentFolderPath = path;
        }
    }
}