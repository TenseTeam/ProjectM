namespace VUDK.Features.Packages.DialogueSystem.Editor.Data.Save
{
    using System.Collections.Generic;
    using System.Numerics;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    [System.Serializable]
    public class DSNodeSaveData
    {
        public string NodeID;
        public string GroupID;
        public Vector2 Position;
        public string Name;
        public string DialogueText;
        public DSDialogueType DialogueType;
        public List<DSChoiceSaveData> Choices;
    }
}