namespace VUDK.Features.More.DialogueSystem.Editor.Constants
{
    public static class DSEditorPaths
    {
        #region FIXED PATHS
        public const string DialoguesEditorFolderPath = "Assets/Code/Scripts/VUDK/Features/More/DialogueSystem/Editor";
        public const string DefaultDialoguesSaveParentFolderPath = "Assets/Code/Scripts/VUDK/Features/More/DialogueSystem";
        #endregion

        #region FOLDER NAMES
        public const string DialogueAssetMainFolderName = "DialougeAssets";
        #endregion

        #region EDITOR PROPERTY PATHS
        public static string DialoguesGraphsAssetPath => $"{DialoguesEditorFolderPath}/Graphs";
        public static string DialoguesEditorResourcesPath => $"{DialoguesEditorFolderPath}/Resources";
        public static string DialogueEditorIconsPath => $"{DialoguesEditorResourcesPath}/EditorIcons";
        public static string StyleSheetsPath => $"{DialoguesEditorResourcesPath}/Styles";
        #endregion

        #region ASSET PROPERTY PATHS
        public static string DialoguesAssetPath => $"{DialoguesSaveParentFolderPath}/{DialogueAssetMainFolderName}";
        public static string DialoguesActorsAssetPath => $"{DialoguesAssetPath}/Actors";
        #endregion

        public static string DialoguesSaveParentFolderPath = DefaultDialoguesSaveParentFolderPath;

        public static void ChangeDialoguesSavePath(string path)
        {
            DialoguesSaveParentFolderPath = path;
        }
    }
}
