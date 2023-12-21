namespace ProjectM.Features.Puzzles.SeekPuzzle.Data
{
    using System.Collections.Generic;
    using UnityEngine.Localization;
    using VUDK.Features.More.DialogueSystem.Data;

    [System.Serializable]
    public class SeekGroupData
    {
        public LocalizedAsset<DSDialogueContainerData> Dialogue;
        public List<SeekFindable> Targets;
    }
}