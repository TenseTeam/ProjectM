namespace VUDK.Features.Packages.DialogueSystem.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class DSDialogueContainerData : ScriptableObject
    {
        public string FileName;
        public SerializableDictionary<string, DSDialogueData> Dialogues = new SerializableDictionary<string, DSDialogueData>();
        public List<DSDialogueData> UngroupedDialouges;

        public void Init(string fileName)
        {
            FileName = fileName;
            Dialogues = new SerializableDictionary<string, DSDialogueData>();
            UngroupedDialouges = new List<DSDialogueData>();
        }
    }
}