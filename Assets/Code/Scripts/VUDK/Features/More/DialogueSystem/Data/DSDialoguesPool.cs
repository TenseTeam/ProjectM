namespace VUDK.Features.More.DialogueSystem.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(DSDialoguesPool), menuName = "VUDK/Dialogue System/Dialogues Pool")]
    public class DSDialoguesPool : ScriptableObject
    {
        public List<DSDialogueContainerData> Pool;
    }
}